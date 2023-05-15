using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.RequestSystem;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Settings;

namespace Kyoto.Commands.BotFactory.SetRegistrationCommand;

public class ChangeThanksRegistrationMessageCommandStep : BaseChangeMessageCommandStep
{
    protected override TemplateMessageTypeValue TemplateMessageType => TemplateMessageTypeValue.ThankRegistering;
    protected override string AdditionalText => "You can use the macro {FirstName} to substitute the client's name.\n";
    
    public ChangeThanksRegistrationMessageCommandStep(IPostService postService, KyotoBotFactorySettings kyotoBotFactorySettings, IRequestService requestService) 
        : base(postService, kyotoBotFactorySettings, requestService)
    {
    }
}