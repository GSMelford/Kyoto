using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.PreparedMessagesSystem;
using Kyoto.Extensions;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using Newtonsoft.Json;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Commands.BotFactory.AddNewsletterCommand;

public class SetTextNewsletterCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    
    public SetTextNewsletterCommandStep(IPostService postService)
    {
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        await _postService.PostAsync(Session, new SendMessageRequest(new SendMessageParameters
        {
            Text = "What message do you want to set? Write it (you can use text MarkdownV2):",
            ChatId = Session.ChatId,
            //ParseMode
        }).ToRequest());
        
        return CommandStepResult.CreateSuccessful();
    }

    protected override Task<CommandStepResult> SetProcessResponseAsync()
    {
        CommandContext.SetAdditionalData(new NewsletterData
        {
            TenantKey = CommandContext.AdditionalData!,
            Text = CommandContext.Message!.Text!
        }.ToJson());
        
        return Task.FromResult(CommandStepResult.CreateSuccessful());
    }
}