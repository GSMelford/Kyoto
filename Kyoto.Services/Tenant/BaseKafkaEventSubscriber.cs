using Confluent.Kafka;
using Kyoto.Domain.Settings;
using Kyoto.Kafka.Interfaces;

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