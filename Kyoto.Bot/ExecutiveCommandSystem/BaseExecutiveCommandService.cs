using Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;
using Microsoft.Extensions.DependencyInjection;
using ExecutiveCommand = Kyoto.Bot.Core.ExecutiveCommandSystem.Models.ExecutiveCommand;
using IExecutiveCommandFactory = Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces.IExecutiveCommandFactory;
using IExecutiveCommandRepository = Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces.IExecutiveCommandRepository;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem;

public abstract class BaseExecutiveCommandService : IExecutiveCommandService
{
    private readonly IExecutiveCommandRepository _executiveCommandRepository;
    private readonly IExecutiveCommandFactory _executiveCommandFactory;
    private readonly IServiceProvider _serviceProvider;

    public BaseExecutiveCommandService(
        IExecutiveCommandRepository executiveCommandRepository,
        IExecutiveCommandFactory executiveCommandFactory,
        IServiceProvider serviceProvider)
    {
        _executiveCommandRepository = executiveCommandRepository;
        _executiveCommandFactory = executiveCommandFactory;
        _serviceProvider = serviceProvider;
    }

    public abstract Task StartExecutiveCommandAsync(Session session, string commandName, object? additionalData = null);

    public abstract Task ProcessExecutiveCommandIfExistAsync(
        Session session,
        Message? message = null,
        CallbackQuery? callbackQuery = null);
    
    private async Task DoExecutiveCommandAsync(Session session, Message? message = null, CallbackQuery? callbackQuery = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var executiveCommand = await _executiveCommandRepository.GetAsync(session);
        var commandStepFactory = _executiveCommandFactory.GetCommandStepFactory(executiveCommand.CommandName);
        var commandContext = CommandContext.Create(session, message, callbackQuery, executiveCommand.AdditionalData);
        
        while (true)
        {
            var commandStep = ActivatorUtilities.CreateInstance(
                scope.ServiceProvider, commandStepFactory.GetCommandStep(executiveCommand.Step)) as BaseCommandStep;

            commandStep!.SetCommandContext(commandContext);
            if (executiveCommand.StepState == CommandStepState.RequestToAction)
            {
                await commandStep.SendActionRequestAsync();
                executiveCommand.SetStepState(CommandStepState.ProcessResponse);
                await UpdateExecutiveCommandAsync(session, executiveCommand, commandContext);
                break;
            }

            if (executiveCommand.StepState == CommandStepState.ProcessResponse)
            {
                await commandStep.ProcessResponseAsync();
                if (commandStep.CommandContext.IsRetry)
                {
                    executiveCommand.SetStep(commandStep.CommandContext.ToRetryStep!.Value);
                    await commandStep.SendRetryActionRequestAsync();
                    break;
                }
            }
            
            if (commandStepFactory.HasNext(executiveCommand.Step))
            {
                executiveCommand.IncreaseStep();
                executiveCommand.ResetState();
                await UpdateExecutiveCommandAsync(session, executiveCommand, commandContext);
            }
            else
            {
                await _executiveCommandRepository.RemoveAsync(session);
                break;
            }
        }
    }

    private Task UpdateExecutiveCommandAsync(Session session, ExecutiveCommand executiveCommand, CommandContext commandContext)
    {
        executiveCommand.SetAdditionalData(commandContext.AdditionalData);
        return _executiveCommandRepository.UpdateAsync(session, executiveCommand);
    }
}