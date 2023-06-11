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

    private const string PreparedMessageEndpoint = "/api/prepared-message";

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
                "⌚ На котрий час\\, Ви хочите відправити повідомлення\\. Напішіть у такому форматі \\- *00\\:00*\\:");
        }
        else if (newsletterData.PostEventCode == PostEventCode.Answer)
        {
            await _postService.SendTextMessageAsync(Session,
                "🤔 На які слова повинен реагувати бот?\nВкажіть їх, розділяючи за допомогою цього знаку \\( *\\,* \\)\\:");
        }
        else
        {
            return CommandStepResult.CreateInterrupt();
        }
        
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var newsletterData = CommandContext.AdditionalData!.ToObject<NewsletterData>();
        PreparedMessageDto preparedMessageDto = null!;
        
        if (newsletterData.PostEventCode == PostEventCode.Time)
        {
            if (DateTime.TryParse(CommandContext.Message!.Text!, out var time))
            {
                preparedMessageDto = new PreparedMessageDto
                {
                    Text = newsletterData.Text,
                    PostEventCode = newsletterData.PostEventCode,
                    TimeToSend = time
                };
            }
            else
            {
                await _postService.SendTextMessageAsync(Session, "Неправильний формат часу!");
                return CommandStepResult.CreateRetry();
            }
        }
        else if(newsletterData.PostEventCode == PostEventCode.Answer)
        {
            preparedMessageDto = new PreparedMessageDto
            {
                Text = newsletterData.Text,
                PostEventCode = newsletterData.PostEventCode,
                KeyWords = CommandContext.Message!.Text!
            };
        }
        
        var isSuccess = await _requestService.SendWithStatusCodeAsync(
            new RequestCreator(HttpMethod.Post, _kyotoBotFactorySettings.ClientBaseUrl + PreparedMessageEndpoint)
                .AddTenantHeader(newsletterData.TenantKey)
                .SetBody(preparedMessageDto.ToJson()).Create());

        if (!isSuccess)
            return CommandStepResult.CreateInterrupt();
        
        await _postService.SendTextMessageAsync(Session, "Повідомлення збережено\\! 🎉");
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetRetryActionRequestAsync()
    {
        await _postService.SendTextMessageAsync(Session, "😨 Щось пішло не так, спробуйте ще раз!");
        return CommandStepResult.CreateSuccessful();
    }
}