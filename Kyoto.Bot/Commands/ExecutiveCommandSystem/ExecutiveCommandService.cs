using Kyoto.Domain.Command;
using Kyoto.Domain.Menu;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Bot.Commands.ExecutiveCommandSystem;

public class ExecutiveCommandService : IExecutiveCommandService
{
    private readonly IExecutiveCommandRepository _executiveCommandRepository;
    private readonly IExecutiveCommandFactory _executiveCommandFactory;
    private readonly IServiceProvider _serviceProvider;

    public ExecutiveCommandService(
        IExecutiveCommandRepository executiveCommandRepository,
        IExecutiveCommandFactory executiveCommandFactory,
        IServiceProvider serviceProvider)
    {
        _executiveCommandRepository = executiveCommandRepository;
        _executiveCommandFactory = executiveCommandFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task StartExecutiveCommandAsync(Session session, CommandType commandType, object? additionalData = null)
    {
        await _executiveCommandRepository.SaveAsync(session, commandType, additionalData);
        await ProcessExecutiveCommandIfExistAsync(session);
    }
    
    public async Task ProcessExecutiveCommandIfExistAsync(
        Session session,
        Message? message = null,
        CallbackQuery? callbackQuery = null)
    {
        if (await _executiveCommandRepository.IsExistAsync(session))
        {
            await DoExecutiveCommandAsync(session, message, callbackQuery);
            return;
        }
        
        switch (message!.Text)
        {
            case MenuButtons.BotManagementButtons.RegisterNewBot:
                await StartExecutiveCommandAsync(session, CommandType.BotRegistration);
                break;
            case MenuButtons.BotManagementButtons.DeployBot:
                await StartExecutiveCommandAsync(session, CommandType.DeployBot);
                break;
        }
    }
    
    private async Task DoExecutiveCommandAsync(Session session, Message? message = null, CallbackQuery? callbackQuery = null)
    {
        using var scope = _serviceProvider.CreateScope();
        var executiveCommand = await _executiveCommandRepository.GetAsync(session);
        var commandStepFactory = _executiveCommandFactory.GetCommandStepFactory(executiveCommand.CommandValue);
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