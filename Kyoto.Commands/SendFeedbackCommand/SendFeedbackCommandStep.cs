using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.FeedbackSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Extensions;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.SendFeedbackCommand;

public class SendFeedbackCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IFeedbackRepository _feedbackRepository;
    
    public SendFeedbackCommandStep(IPostService postService, IFeedbackRepository feedbackRepository)
    {
        _postService = postService;
        _feedbackRepository = feedbackRepository;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        if (!await _feedbackRepository.IsFeedbackEnableAsync())
        {
            await _postService.SendTextMessageAsync(Session, "😞 Нажаль власник не увімкнув можливість збору фідбеку");
            return CommandStepResult.CreateInterrupt();
        }
        
        await _postService.SendTextMessageAsync(Session, "✍️ Напішить Ваш відгук наступним повідомленням:");
        return CommandStepResult.CreateSuccessful();
    }

    protected override Task<CommandStepResult> SetProcessResponseAsync()
    {
        var feedbackData = new FeedbackData(CommandContext.Message!.Text!);
        CommandContext.SetAdditionalData(feedbackData.ToJson());
        return Task.FromResult(CommandStepResult.CreateSuccessful());
    }
}