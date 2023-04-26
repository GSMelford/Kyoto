using Kyoto.Domain.Command;
using Kyoto.Domain.Menu;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Bot.Services.Command.ExecutiveCommandSystem;

public class ExecutiveCommandService : IExecutiveCommandService
{
    private readonly IExecutiveCommandRepository _executiveCommandRepository;
    private readonly IExecutiveCommandFactory _executiveCommandFactory;
    private readonly IPostService _postService;
    private readonly IServiceProvider _serviceProvider;

    public ExecutiveCommandService(
        IExecutiveCommandRepository executiveCommandRepository,
        IExecutiveCommandFactory executiveCommandFactory,
        IPostService postService, 
        IServiceProvider serviceProvider)
    {
        _executiveCommandRepository = executiveCommandRepository;
        _executiveCommandFactory = executiveCommandFactory;
        _postService = postService;
        _serviceProvider = serviceProvider;
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
        }
    }
    
    public async Task StartExecutiveCommandAsync(Session session, CommandType commandType, object? additionalData = null)
    {
        await _executiveCommandRepository.SaveAsync(session, commandType, additionalData);
        await ProcessExecutiveCommandIfExistAsync(session);
    }

    private async Task DoExecutiveCommandAsync(Session session, Message? message = null, CallbackQuery? callbackQuery = null)
    {
        while (true)
        {
            var executiveCommand = await _executiveCommandRepository.GetAsync(session);
            var commandStepFactory = _executiveCommandFactory.GetCommandStepFactory(executiveCommand.CommandValue);

            using var scope = _serviceProvider.CreateScope();
            var commandStep = ActivatorUtilities.CreateInstance(
                scope.ServiceProvider, commandStepFactory.GetCommandStep(executiveCommand.Step)) as ICommandStep;

            var commandContext = CommandContext.Create(session, message, callbackQuery, executiveCommand.AdditionalData);
            switch (executiveCommand.StepState)
            {
                case CommandStepState.RequestToAction:
                    await DoRequestToActionAsync(session, commandStep!, executiveCommand, commandContext);
                    return;
                case CommandStepState.ProcessResponse:
                    await DoProcessResponseAsync(session, commandStep!, commandContext);
                    break;
            }

            if (commandStepFactory.HasNext(executiveCommand.Step))
            {
                executiveCommand.IncreaseStep();
                executiveCommand.ResetState();
                await _executiveCommandRepository.UpdateAsync(session, executiveCommand);
                continue;
            }

            await _executiveCommandRepository.RemoveAsync(session);
            break;
        }
    }

    private async Task DoRequestToActionAsync(
        Session session,
        ICommandStep commandStep,
        ExecutiveCommand executiveCommand,
        CommandContext commandContext)
    {
        await commandStep.SendActionRequestAsync(commandContext);
        executiveCommand.SetStepState(CommandStepState.ProcessResponse);
        await _executiveCommandRepository.UpdateAsync(session, executiveCommand);
    }
    
    private async Task DoProcessResponseAsync(
        Session session,
        ICommandStep commandStep,
        CommandContext commandContext)
    {
        var result = await commandStep.ProcessResponseAsync(commandContext);
        if (result is { IsSuccessful: true, IsRetry: true })
        {
            await commandStep.SendActionRequestAsync(commandContext);
            return;
        }

        if (result.IsSuccessful)
        {
            await commandStep.FinalAction(commandContext);
        }
        else
        {
            await _postService.SendTextMessageAsync(session,
                $"Something went wrong...\nReason: {result.ErrorMessage}\nContact support or try again.");

            await _executiveCommandRepository.RemoveAsync(session);
        }
    }
}