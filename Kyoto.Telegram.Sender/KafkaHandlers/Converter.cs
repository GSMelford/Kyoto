using Kyoto.Kafka.Event;
using Kyoto.Telegram.Sender.Domain;

namespace Kyoto.Telegram.Sender.KafkaHandlers;

public static class Converter
{
    public static RequestModel ToDomain(this RequestEvent requestEvent)
    {
        return RequestModel.Create(
            requestEvent.Endpoint, 
            requestEvent.HttpMethod,
            requestEvent.Parameters);
    }
}