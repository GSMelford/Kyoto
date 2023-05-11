using Confluent.Kafka;
using Kyoto.Kafka.Interfaces;
using Kyoto.Settings;

namespace Kyoto.Services.Tenant;

public abstract class BaseKafkaEventSubscriber : IKafkaEventSubscriber
{
    protected readonly IKafkaConsumerFactory KafkaConsumerFactory;
    protected readonly ConsumerConfig ConsumerConfig;

    protected BaseKafkaEventSubscriber(IKafkaConsumerFactory kafkaConsumerFactory, KafkaSettings kafkaSettings)
    {
        KafkaConsumerFactory = kafkaConsumerFactory;
        ConsumerConfig = new ConsumerConfig
        {
            BootstrapServers = kafkaSettings.BootstrapServers
        };
    }

    public abstract Task SubscribeAsync(string tenantKey);
}