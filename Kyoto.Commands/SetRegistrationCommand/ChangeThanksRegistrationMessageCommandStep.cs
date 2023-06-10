using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.RequestSystem;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Settings;

namespace Kyoto.Commands.SetRegistrationCommand;

public class ChangeThanksRegistrationMessageCommandStep : BaseChangeMessageCommandStep
{
    protected override TemplateMessageTypeValue TemplateMessageType => TemplateMessageTypeValue.ThankRegistering;
    protected override string AdditionalText => "Ви можете використовувати макрос {FirstName}\\, щоб замінити ім’я клієнта\\.\n";
    
    public ChangeThanksRegistrationMessageCommandStep(IPostService postService, KyotoBotFactorySettings kyotoBotFactorySettings, IRequestService requestService) 
        : base(postService, kyotoBotFactorySettings, requestService)
    {
    }
}