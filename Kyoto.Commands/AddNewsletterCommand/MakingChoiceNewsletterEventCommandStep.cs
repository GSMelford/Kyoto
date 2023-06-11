using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.PreparedMessagesSystem;
using Kyoto.Extensions;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.AddNewsletterCommand;

public class MakingChoiceNewsletterEventCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IPreparedMessagesRepository _preparedMessagesRepository;

    public MakingChoiceNewsletterEventCommandStep(IPostService postService, IPreparedMessagesRepository preparedMessagesRepository)
    {
        _preparedMessagesRepository = preparedMessagesRepository;
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var keyboard = new InlineKeyboardMarkup();
        foreach (var postEvent in await _preparedMessagesRepository.GetEventsAsync())
        {
            keyboard.Add(new InlineKeyboardButton
            {
                Text = postEvent.Name,
                CallbackData = ((int)postEvent.Code).ToString()
            });
        }
        
        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = "📃 Виберіть подію надсилання:",
            ChatId = Session.ChatId,
            ReplyMarkup = keyboard
        }).ToRequest());
        
        return CommandStepResult.CreateSuccessful();
    }

    protected override Task<CommandStepResult> SetProcessResponseAsync()
    {
        string tenantKey = CommandContext.AdditionalData!;
        var postEventCode = Enum.Parse<PostEventCode>(CommandContext.CallbackQuery!.Data!);
        
        CommandContext.SetAdditionalData(new NewsletterData
        {
            TenantKey = tenantKey,
            PostEventCode = postEventCode
        }.ToJson());
        
        return Task.FromResult(CommandStepResult.CreateSuccessful());
    }
}