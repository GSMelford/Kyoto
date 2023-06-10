using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Extensions;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.SendFeedbackCommand;

public class SendFeedbackCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    
    public SendFeedbackCommandStep(IPostService postService)
    {
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
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