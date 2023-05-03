using Confluent.Kafka;

namespace Kyoto.Kafka.Interfaces;

public interface IKafkaProducer<TKey> : IDisposable
{
    Task<DeliveryResult<TKey?, string>> ProduceAsync<TEvent>(TEvent eventData, string topic, TKey? key = default);
}