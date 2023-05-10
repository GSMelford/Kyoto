using Kyoto.Dal.CommonRepositories.ExecuteCommandSystem;
using Kyoto.Domain.ExecutiveCommand;
using Kyoto.Domain.ExecutiveCommand.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;
using Microsoft.Extensions.DependencyInjection;
using IExecutiveCommandFactory = Kyoto.Domain.ExecutiveCommand.Interfaces.IExecutiveCommandFactory;

namespace Kyoto.Services.ExecuteCommand;

public abstract class BaseExecutiveCommandService : IExecutiveCommandService
{
    protected readonly IExecutiveCommandRepository ExecutiveCommandRepository;
    protected readonly IExecutiveCommandFactory ExecutiveCommandFactory;
    protected readonly IServiceProvider ServiceProvider;

    protected BaseExecutiveCommandService(
        IExecutiveCommandRepository executiveCommandRepository,
        IExecutiveCommandFactory executiveCommandFactory,
        IServiceProvider serviceProvider)
    {
        ExecutiveCommandRepository = executiveCommandRepository;
        ExecutiveCommandFactory = executiveCommandFactory;
        ServiceProvider = serviceProvider;
    }

    public abstract Task StartExecutiveCommandAsync(Session session, string commandName, object? additionalData = null);

    public abstract Task ProcessExecutiveCommandIfExistsAsync(
        Session session,
        Message? message = null,
        CallbackQuery? callbackQuery = null);

    protected async Task DoExecutiveCommandAsync(Session session, Message? message = null, CallbackQuery? callbackQuery = null)
    {
        using var scope = ServiceProvider.CreateScope();
        var executiveCommand = await ExecutiveCommandRepository.GetAsync(session);
        var commandStepFactory = ExecutiveCommandFactory.GetCommandStepFactory(executiveCommand.CommandName);
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
                await ExecutiveCommandRepository.RemoveAsync(session);
                break;
            }
        }
    }

    private Task UpdateExecutiveCommandAsync(Session session, ExecutiveCommand executiveCommand, CommandContext commandContext)
    {
        executiveCommand.SetAdditionalData(commandContext.AdditionalData);
        return ExecutiveCommandRepository.UpdateAsync(session, executiveCommand);
    }
}