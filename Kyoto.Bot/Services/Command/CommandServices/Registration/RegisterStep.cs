using Kyoto.Bot.Services.Authorization;
using Kyoto.Bot.Services.Menu;
using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.Authorization;
using Kyoto.Domain.BotUser;
using Kyoto.Domain.Command;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.Services.Command.CommandServices.Registration;

public class RegisterStep : ICommandStep
{
    private readonly IPostService _postService;
    private readonly IAuthorizationService _authorizationService;
    private readonly MenuPanelPostService _menuPanelPostService;

    public RegisterStep(
        IPostService postService,
        IAuthorizationService authorizationService,
        MenuPanelPostService menuPanelPostService)
    {
        _postService = postService;
        _authorizationService = authorizationService;
        _menuPanelPostService = menuPanelPostService;
    }

    public async Task SendActionRequestAsync(CommandContext commandContext)
    {
        string text = commandContext.IsRetry
            ? "Let's try to get to know each other again!"
            : "Hello! We haven't met before, let's get to know each other 👉👈⬇️";
        
        var request = new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = commandContext.Session.ChatId,
            ReplyMarkup = new ReplyKeyboardMarkup { OneTimeKeyboard = true }
                .Add(new KeyboardButton
                {
                    Text = "Register - and start creating your bot! 😎",
                    RequestContact = true
                })
        }).ToRequest();
        
        await _postService.SendAsync(commandContext.Session.Id, request);
    }

    public async Task<CommandStepResult> ProcessResponseAsync(CommandContext commandContext)
    {
        if (commandContext.Message!.Contact is null)
        {
            return CommandStepResult.CreateRetry("First you need to register!");
        }
        
        var user = commandContext.Message!.ToUserDomain();
        await _authorizationService.RegisterAsync(user);
        return CommandStepResult.CreateSuccessful();
    }

    public async Task FinalAction(CommandContext commandContext)
    {
        await _postService.SendTextMessageAsync(
            commandContext.Session, $"Thank you for registering, {commandContext.Message!.FromUser!.FirstName}! 💞");
        await _menuPanelPostService.SendBotManagementAsync(commandContext.Session);
    }
}