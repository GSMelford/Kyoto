using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Commands.BotFactory.SetRegistrationCommand;

public class ChangeThanksRegistrationMessageCommandStep : BaseChangeMessageCommandStep
{
    protected override TemplateMessageTypeValue TemplateMessageType => TemplateMessageTypeValue.ThankRegistering;
    protected override string AdditionalText => "You can use the macro {FirstName} to substitute the client's name.\n";

    public ChangeThanksRegistrationMessageCommandStep(IPostService postService, IKafkaProducer<string> kafkaProducer) 
        : base(postService, kafkaProducer)
    {
    }
}