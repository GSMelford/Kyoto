using Kyoto.Bot.Services.Menu;
using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.Authorization;
using Kyoto.Domain.PostSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.Services.Command.CommandServices.Registration;

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
    
    public override async Task SendActionRequestAsync()
    {
        string text = CommandContext.IsRetry
            ? "Let's try to get to know each other again!"
            : "Hello! We haven't met before, let's get to know each other 👉👈⬇️";
        
        var request = new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = CommandContext.Session.ChatId,
            ReplyMarkup = new ReplyKeyboardMarkup { OneTimeKeyboard = true }
                .Add(new KeyboardButton
                {
                    Text = "Register - and start creating your bot! 😎",
                    RequestContact = true
                })
        }).ToRequest();
        
        await _postService.SendAsync(CommandContext.Session.Id, request);
    }

    public override async Task ProcessResponseAsync()
    {
        if (CommandContext.Message!.Contact is null)
        {
            CommandContext.SetRetry(errorMessage: "First you need to register!");
        }
        else
        {
            var user = CommandContext.Message!.ToUserDomain();
            await _authorizationService.RegisterAsync(user);
            
            await _postService.SendTextMessageAsync(
                CommandContext.Session, $"Thank you for registering, {CommandContext.Message!.FromUser!.FirstName}! 💞");
            await _menuPanelPostService.SendBotManagementAsync(CommandContext.Session);
        }
    }
}