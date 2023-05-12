using Kyoto.Domain.BotFactory.Authorization.Interfaces;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Services.BotFactory.Menu;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.BotFactory.RegistrationCommand;

public class RegisterStep : BaseCommandStep
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

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        await SendWelcomeMessageAsync(
            "Kon'nichiwa! 👋\nLet's get to know each other!\nI'm Kyoto, like a city in Japan ⛩\nAnd what is your name?😉");
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        if (CommandContext.Message!.Contact is null)
        {
            return CommandStepResult.CreateRetry();
        }
        
        var user = CommandContext.Message!.ToUserDomain();
        await _authorizationService.RegisterAsync(user);
            
        await _postService.SendTextMessageAsync(Session, 
            $"Nice to meet you, {CommandContext.Message!.FromUser!.FirstName}! 💞");
            
        await _postService.SendTextMessageAsync(Session, "Now a little about myself..."); //TODO: Text
        await _menuPanelPostService.SendBotManagementAsync(Session);
        
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetRetryActionRequestAsync()
    {
        await SendWelcomeMessageAsync("Press the button to share the contact so that we can get to know each other ☺️");
        return CommandStepResult.CreateSuccessful();
    }

    private Task SendWelcomeMessageAsync(string text)
    {
        var request = new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = Session.ChatId,
            ReplyMarkup = new ReplyKeyboardMarkup { OneTimeKeyboard = true, ResizeKeyboard = true }
                .Add(new KeyboardButton
                {
                    Text = $"I'm {CommandContext.AdditionalData}! (Share contact with Kyoto) 👋",
                    RequestContact = true
                })
        }).ToRequest();
        
        return _postService.PostAsync(Session, request);
    }
}