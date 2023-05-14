using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Commands.BotFactory.SetRegistrationCommand;

public class ChangeHelloMessageCommandStep : BaseChangeMessageCommandStep
{
    protected override TemplateMessageTypeValue TemplateMessageType => TemplateMessageTypeValue.StartMessage;
    
    public ChangeHelloMessageCommandStep(IPostService postService, IKafkaProducer<string> kafkaProducer) 
        : base(postService, kafkaProducer)
    {
    }
}