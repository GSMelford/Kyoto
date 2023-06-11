using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Extensions;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.AddNewsletterCommand;

public class SetTextNewsletterCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    
    public SetTextNewsletterCommandStep(IPostService postService)
    {
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        await _postService.SendTextMessageAsync(Session,
            "🤔 Напишіть повідомлення \\(Mожете використовувати *MarkdownV2* 😋\\)\\:");
        
        return CommandStepResult.CreateSuccessful();
    }

    protected override Task<CommandStepResult> SetProcessResponseAsync()
    {
        var newsletterData = CommandContext.AdditionalData!.ToObject<NewsletterData>();
        newsletterData.Text = CommandContext.Message!.Text!;
        
        CommandContext.SetAdditionalData(newsletterData.ToJson());
        
        return Task.FromResult(CommandStepResult.CreateSuccessful());
    }
}