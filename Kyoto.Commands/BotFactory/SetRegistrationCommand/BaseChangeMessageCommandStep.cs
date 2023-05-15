using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.RequestSystem;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Dto.TemplateMessageSystem;
using Kyoto.Extensions;
using Kyoto.Services.CommandSystem;
using Kyoto.Settings;

namespace Kyoto.Commands.BotFactory.SetRegistrationCommand;

public abstract class BaseChangeMessageCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly KyotoBotFactorySettings _kyotoBotFactorySettings;
    private readonly IRequestService _requestService;

    private const string TEMPLATE_MESSAGE_ENDPOINT = "/api/template-message";
    
    protected abstract TemplateMessageTypeValue TemplateMessageType { get; }
    protected virtual string AdditionalText { get; } = string.Empty;

    protected BaseChangeMessageCommandStep(
        IPostService postService,
        KyotoBotFactorySettings kyotoBotFactorySettings,
        IRequestService requestService)
    {
        _postService = postService;
        _kyotoBotFactorySettings = kyotoBotFactorySettings;
        _requestService = requestService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var tenantKey = CommandContext.AdditionalData!;
        var templateMessageDto = await _requestService.SendAsync<TemplateMessageDto>(
            new RequestCreator(HttpMethod.Get, _kyotoBotFactorySettings.ClientBaseUrl + TEMPLATE_MESSAGE_ENDPOINT)
                .AddTenantHeader(tenantKey)
                .AddParameters(new Dictionary<string, string> { { "type", ((int)TemplateMessageType).ToString() } })
                .Create());

        await _postService.SendTextMessageAsync(Session, $"Code: {templateMessageDto.Code}\nDescription: {templateMessageDto.Description}\nText: {templateMessageDto.Text}");
        await _postService.SendTextMessageAsync(Session, $"{AdditionalText}✍️ Enter new text:");
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var tenantKey = CommandContext.AdditionalData!;
        var newText = CommandContext.Message!.Text!;

        var isSuccess = await _requestService.SendWithStatusCodeAsync(new RequestCreator(HttpMethod.Patch, _kyotoBotFactorySettings.ClientBaseUrl + TEMPLATE_MESSAGE_ENDPOINT)
                .AddTenantHeader(tenantKey)
                .AddParameters(new Dictionary<string, string>
                {
                    { "type", ((int)TemplateMessageType).ToString() },
                    { "text", newText }
                }).Create());

        if (!isSuccess) {
            await _postService.SendTextMessageAsync(Session, "😨 Something went wrong.");
            return CommandStepResult.CreateInterrupt();
        }
        
        await _postService.SendTextMessageAsync(Session, "🎉 The text has been updated!");
        return CommandStepResult.CreateSuccessful();
    }
}