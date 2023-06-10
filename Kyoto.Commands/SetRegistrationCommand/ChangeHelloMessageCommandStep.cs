using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.RequestSystem;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Settings;

namespace Kyoto.Commands.SetRegistrationCommand;

public class ChangeHelloMessageCommandStep : BaseChangeMessageCommandStep
{
    protected override TemplateMessageTypeValue TemplateMessageType => TemplateMessageTypeValue.StartMessage;


    public ChangeHelloMessageCommandStep(IPostService postService, KyotoBotFactorySettings kyotoBotFactorySettings, IRequestService requestService)
        : base(postService, kyotoBotFactorySettings, requestService)
    {
    }
}