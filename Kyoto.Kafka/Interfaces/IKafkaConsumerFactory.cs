using Confluent.Kafka;
using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Interfaces;

public interface IKafkaConsumerFactory : IDisposable
{
    void Subscribe<TEvent, THandler>(
        ConsumerConfig? config = null,
        string? topicPrefix = null,
        string? groupId = null,
        bool? enableAutoCommit = true)
        where THandler : class, IKafkaHandler<TEvent>
        where TEvent : BaseEvent;
}