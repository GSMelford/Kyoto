using Kyoto.Bot.Commands.ExecutiveCommandSystem;
using Kyoto.Bot.Services.Menu;
using Kyoto.Bot.Services.PostSystem;
using Kyoto.Domain.Authorization;
using Kyoto.Domain.PostSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.Commands.RegistrationCommand;

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

    protected override Task SetActionRequestAsync()
    {
        return SendWelcomeMessageAsync("Kon'nichiwa! 👋\nLet's get to know each other!\nI'm Kyoto, like a city in Japan ⛩\nAnd what is your name?😉");
    }

    protected override async Task SetProcessResponseAsync()
    {
        if (CommandContext.Message!.Contact is null)
        {
            CommandContext.SetRetry();
            return;
        }
        
        var user = CommandContext.Message!.ToUserDomain();
        await _authorizationService.RegisterAsync(user);
            
        await _postService.SendTextMessageAsync(CommandContext.Session, 
            $"Nice to meet you, {CommandContext.Message!.FromUser!.FirstName}! 💞");
            
        await _postService.SendTextMessageAsync(CommandContext.Session, "Now a little about myself..."); //TODO: Text
        await _menuPanelPostService.SendBotManagementAsync(CommandContext.Session);
    }

    protected override Task SetRetryActionRequestAsync()
    {
        return SendWelcomeMessageAsync("Press the button to share the contact so that we can get to know each other ☺️");
    }

    private Task SendWelcomeMessageAsync(string text)
    {
        var request = new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = CommandContext.Session.ChatId,
            ReplyMarkup = new ReplyKeyboardMarkup { OneTimeKeyboard = true, ResizeKeyboard = true }
                .Add(new KeyboardButton
                {
                    Text = $"I'm {CommandContext.AdditionalData}! (Share contact with Kyoto) 👋",
                    RequestContact = true
                })
        }).ToRequest();
        
        return _postService.PostAsync(CommandContext.Session, request);
    }
}