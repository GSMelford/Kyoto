using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.Tenant;
using Kyoto.Settings;
using Kyoto.Telegram.Sender.KafkaHandlers;

namespace Kyoto.Telegram.Sender;

public class KafkaEventSubscriber : BaseKafkaEventSubscriber
{
    public KafkaEventSubscriber(IKafkaConsumerFactory kafkaConsumerFactory, KafkaSettings kafkaSettings)
        : base(kafkaConsumerFactory, kafkaSettings)
    {
    }

    public override Task SubscribeAsync(string tenantKey)
    {
        return KafkaConsumerFactory.SubscribeAsync<RequestEvent, RequestHandler>(ConsumerConfig, topicPrefix: tenantKey);
    }
}