using Kyoto.Domain.BotFactory.Authorization.Interfaces;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.CommonCommnad.RegistrationCommand;

public class RegisterStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IAuthorizationService _authorizationService;
    private readonly ITemplateMessageRepository _templateRepository;
    private readonly IMenuService _menuService;

    public RegisterStep(
        IPostService postService,
        IAuthorizationService authorizationService,
        ITemplateMessageRepository templateRepository, 
        IMenuService menuService)
    {
        _postService = postService;
        _authorizationService = authorizationService;
        _templateRepository = templateRepository;
        _menuService = menuService;
    }
    
    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var templateMessage = await _templateRepository.GetAsync(TemplateMessageTypeValue.StartMessage);
        CommandContext.SetAdditionalData(CommandContext.Message!.FromUser!.FirstName);
        await SendWelcomeMessageAsync(templateMessage.Text);
        return CommandStepResult.CreateSuccessful();
    }
    
    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        if (CommandContext.Message!.Contact is null)
            return CommandStepResult.CreateRetry(); 
        
        var templateMessage = await _templateRepository.GetAsync(TemplateMessageTypeValue.ThankRegistering);
        
        var user = CommandContext.Message!.ToUserDomain();
        await _authorizationService.RegisterAsync(user);
        await _postService.SendTextMessageAsync(Session,
            templateMessage.Text.Replace("{FirstName}", CommandContext.Message.FromUser!.FirstName));
        
        templateMessage = await _templateRepository.GetAsync(TemplateMessageTypeValue.AboutBot);
        await _postService.SendTextMessageAsync(Session, templateMessage.Text);
        await _menuService.SendHomeMenuAsync(Session);
        
        return CommandStepResult.CreateSuccessful();
    }
    
    protected override async Task<CommandStepResult> SetRetryActionRequestAsync()
    {
        var templateMessage = await _templateRepository.GetAsync(TemplateMessageTypeValue.RetryRegistration);
        await SendWelcomeMessageAsync(templateMessage.Text);
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
                    Text = $"I'm {CommandContext.AdditionalData}! (Share contact with bot) 👋",
                    RequestContact = true
                })
        }).ToRequest();
        
        return _postService.PostAsync(Session, request);
    }
}