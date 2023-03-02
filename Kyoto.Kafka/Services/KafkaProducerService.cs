using Confluent.Kafka;
using Kyoto.Kafka.Interfaces;
using Newtonsoft.Json;

namespace Kyoto.Kafka.Services;

public class KafkaProducerService<TKey> : IKafkaProducer<TKey>
{
    private readonly IProducer<TKey?, string> _producer;
    
    public KafkaProducerService(ProducerConfig config)
    {
        _producer = new ProducerBuilder<TKey?, string>(config).Build();
    }

    public async Task<DeliveryResult<TKey?, string>> ProduceAsync<TEvent>(TEvent eventData, string? topic = null, TKey? key = default)
    {
        if (string.IsNullOrEmpty(topic)) {
            topic = typeof(TEvent).Name;
        }
        
        DeliveryResult<TKey?, string> deliveryResult = await _producer.ProduceAsync(topic, new Message<TKey?, string>
        {
            Key = key,
            Value = JsonConvert.SerializeObject(eventData)
        });
        
        _producer.Flush();
        return deliveryResult;
    }

    public void Dispose()
    {
        _producer.Dispose();
        GC.SuppressFinalize(this);
    }
}