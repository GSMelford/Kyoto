using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.FeedbackSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Processors;
using Kyoto.Extensions;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.SendFeedbackCommand;

public class SetIsAnonymousFeedbackCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IFeedbackRepository _feedbackRepository;

    public SetIsAnonymousFeedbackCommandStep(IPostService postService, IFeedbackRepository feedbackRepository)
    {
        _postService = postService;
        _feedbackRepository = feedbackRepository;
    }
    
    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        await _postService.SendConfirmationMessageAsync(Session, "🤔 Бажаєте залишитися анонімним?");
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var feedbackData = CommandContext.AdditionalData!.ToObject<FeedbackData>();
        
        if (CommandContext.CallbackQuery!.Data == CallbackQueryButtons.Confirmation) {
            feedbackData.IsAnonymous = true;
        }
        else {
            feedbackData.TelegramId = CommandContext.CallbackQuery!.From.Id;
        }

        await _feedbackRepository.SaveFeedbackAsync(
            feedbackData.Text, feedbackData.IsAnonymous, feedbackData.TelegramId, feedbackData.Stars);

        await _postService.SendTextMessageAsync(Session, "Дякуємо за ваш відгук\\! ❤️");
        return CommandStepResult.CreateSuccessful();
    }
}