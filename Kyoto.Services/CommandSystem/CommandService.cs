using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Services.CommandSystem;

public class CommandService : ICommandService
{
    private readonly ICommandRepository _commandRepository;
    private readonly ICommandFactory _commandFactory;
    private readonly IServiceProvider _serviceProvider;
    private readonly ICommandSet _commandSet;

    public CommandService(
        ICommandRepository commandRepository,
        ICommandFactory commandFactory,
        IServiceProvider serviceProvider, 
        ICommandSet commandSet)
    {
        _commandRepository = commandRepository;
        _commandFactory = commandFactory;
        _serviceProvider = serviceProvider;
        _commandSet = commandSet;
    }

    public async Task<string> CancelCommandAsync(Session session)
    {
        var command = await _commandRepository.GetAsync(session);
        await _commandRepository.RemoveAsync(session);
        return command.Name;
    }
    
    public async Task ProcessCommandAsync(
        Session session, 
        string commandName,
        Message? message = null,
        CallbackQuery? callbackQuery = null)
    {
        if (!await _commandRepository.IsCommandExistsAsync(session))
        {
            if (_commandSet.IsExists(commandName)) {
                await _commandRepository.TrySaveCommandAsync(session, commandName);
            }
            else {
                return;
            }
        }
        
        await ExecuteCommandAsync(session, CommandContext.Create(message, callbackQuery));
    }
    
    private async Task ExecuteCommandAsync(Session session, CommandContext commandContext)
    {
        using var scope = _serviceProvider.CreateScope();
        var command = await _commandRepository.GetAsync(session);
        var commandStepFactory = _commandFactory.GetCommandStepFactory(command.Name);
        commandContext.SetAdditionalData(command.AdditionalData);
        
        while (true)
        {
            var commandStep = (BaseCommandStep)ActivatorUtilities.CreateInstance(
                scope.ServiceProvider, 
                commandStepFactory.GetCommandStep(command.Step));
            
            commandStep.SetCommandContext(commandContext);
            commandStep.SetSession(session);
            
            if (command.State == CommandState.RequestToAction)
            {
                var result = await commandStep.SendActionRequestAsync();
                command.SetState(CommandState.ProcessResponse);
                await UpdateCommandAsync(session, command, commandContext);

                if (result.IsInterrupt) {
                    await _commandRepository.RemoveAsync(session);
                }
                
                break;
            }

            if (command.State == CommandState.ProcessResponse)
            {
                var result = await commandStep.ProcessResponseAsync();
                
                if (result.IsRetry)
                {
                    command.SetStep(result.ToRetryStep ?? command.Step);
                    result = await commandStep.SendRetryActionRequestAsync();
                    if (result.IsInterrupt) {
                        await _commandRepository.RemoveAsync(session);
                    }
                    await UpdateCommandAsync(session, command, commandContext);
                    break;
                }
            }
            
            if (commandStepFactory.HasNext(command.Step))
            {
                command.IncreaseStep();
                command.ResetState();
                await UpdateCommandAsync(session, command, commandContext);
            }
            else
            {
                await _commandRepository.RemoveAsync(session);
                break;
            }
        }
    }

    private Task UpdateCommandAsync(Session session, Command command, CommandContext commandContext)
    {
        command.SetAdditionalData(commandContext.AdditionalData);
        return _commandRepository.UpdateCommandAsync(session, command);
    }
}