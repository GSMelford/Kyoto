using Kyoto.Bot.Services.Command.CommandServices;
using Kyoto.Domain.Command;
using Kyoto.Domain.Menu;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;
using Newtonsoft.Json;

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
                executiveCommand.SetAdditionalData(commandContext.AdditionalData);
                executiveCommand.SetStepState(CommandStepState.ProcessResponse);
                await _executiveCommandRepository.UpdateAsync(session, executiveCommand);
                return;
            }

            if (executiveCommand.StepState == CommandStepState.ProcessResponse)
            {
                await commandStep.ProcessResponseAsync();
                executiveCommand.SetAdditionalData(commandContext.AdditionalData);
                
                if (commandStep.CommandContext.IsRetry)
                {
                    if (commandStep.CommandContext.ToRetryStep is not null)
                    {
                        executiveCommand.SetStep(commandStep.CommandContext.ToRetryStep.Value);
                    }
                    
                    await _executiveCommandRepository.UpdateAsync(session, executiveCommand);
                    await SendErrorMessageAsync(session, commandStep.CommandContext.ErrorMessage!);
                    return;
                }
                
                if (commandStep.CommandContext.IsFailure)
                {
                    await _executiveCommandRepository.RemoveAsync(session);
                    await SendErrorMessageAsync(session, commandStep.CommandContext.ErrorMessage!);
                }
                
                await _executiveCommandRepository.UpdateAsync(session, executiveCommand);
            }
            
            if (commandStepFactory.HasNext(executiveCommand.Step))
            {
                executiveCommand.IncreaseStep();
                executiveCommand.ResetState();
                await _executiveCommandRepository.UpdateAsync(session, executiveCommand);
            }
            else
            {
                await _executiveCommandRepository.RemoveAsync(session);
                break;
            }
        }
    }

    private Task SendErrorMessageAsync(Session session, string text)
    {
        return _postService.SendTextMessageAsync(session, text);
    }
}