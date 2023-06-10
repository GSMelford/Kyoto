using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.RequestSystem;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Settings;

namespace Kyoto.Commands.SetRegistrationCommand;

public class ChangeRetryRegistrationMessageCommandStep : BaseChangeMessageCommandStep
{
    private readonly IPostService _postService;
    
    protected override TemplateMessageTypeValue TemplateMessageType => TemplateMessageTypeValue.RetryRegistration;
    
    public ChangeRetryRegistrationMessageCommandStep(IPostService postService, KyotoBotFactorySettings kyotoBotFactorySettings, IRequestService requestService) 
        : base(postService, kyotoBotFactorySettings, requestService)
    {
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var result = await base.SetProcessResponseAsync();
        await _postService.SendTextMessageAsync(Session, "😊 Команду успішно змінено");
        return result;
    }
}