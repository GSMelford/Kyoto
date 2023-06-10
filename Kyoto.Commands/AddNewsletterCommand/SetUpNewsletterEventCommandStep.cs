using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.PreparedMessagesSystem;
using Kyoto.Domain.RequestSystem;
using Kyoto.Dto.PreparedMessageSystem;
using Kyoto.Extensions;
using Kyoto.Services.CommandSystem;
using Kyoto.Settings;

namespace Kyoto.Commands.AddNewsletterCommand;

public class SetUpNewsletterEventCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IRequestService _requestService;
    private readonly KyotoBotFactorySettings _kyotoBotFactorySettings;

    private const string PREPARED_MESSAGE_ENDPOINT = "/api/prepared-message";

    public SetUpNewsletterEventCommandStep(
        IPostService postService, 
        IRequestService requestService, 
        KyotoBotFactorySettings kyotoBotFactorySettings)
    {
        _postService = postService;
        _requestService = requestService;
        _kyotoBotFactorySettings = kyotoBotFactorySettings;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var newsletterData = CommandContext.AdditionalData!.ToObject<NewsletterData>();
        
        if (newsletterData.PostEventCode == PostEventCode.Time)
        {
            await _postService.SendTextMessageAsync(Session,
                "⌚ Please write the time when you want to send this message in the format 00:00 :");
        }
        
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var newsletterData = CommandContext.AdditionalData!.ToObject<NewsletterData>();

        if (newsletterData.PostEventCode == PostEventCode.Time)
        {
            if (DateTime.TryParse(CommandContext.Message!.Text!, out var time))
            {
                var isSuccess = await _requestService.SendWithStatusCodeAsync(
                    new RequestCreator(HttpMethod.Post, _kyotoBotFactorySettings.ClientBaseUrl + PREPARED_MESSAGE_ENDPOINT)
                        .AddTenantHeader(newsletterData.TenantKey)
                        .SetBody(new PreparedMessageDto
                        {
                            Text = newsletterData.Text,
                            PostEventCode = newsletterData.PostEventCode,
                            TimeToSend = time
                        }.ToJson()).Create());

                if (!isSuccess)
                {
                    return CommandStepResult.CreateInterrupt();
                }
            }
            else
            {
                await _postService.SendTextMessageAsync(Session, "The time format is incorrect!");
                return CommandStepResult.CreateRetry();
            }
        }
        
        await _postService.SendTextMessageAsync(Session, "Prepared message added! 🎉");
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetRetryActionRequestAsync()
    {
        await _postService.SendTextMessageAsync(Session, "Try again! The time must be in this format 00:00!");
        return CommandStepResult.CreateSuccessful();
    }
}