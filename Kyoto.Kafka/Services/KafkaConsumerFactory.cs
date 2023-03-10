using Confluent.Kafka;
using Kyoto.Kafka.Interfaces;
using Kyoto.Kafka.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kyoto.Kafka.Services;

public class KafkaConsumerFactory : IKafkaConsumerFactory
{
    private readonly ILogger<IKafkaConsumerFactory>? _logger;
    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<string, IKafkaConsumerService> _consumers = new();

    public KafkaConsumerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = _serviceProvider.GetService<ILogger<IKafkaConsumerFactory>>();
    } 
    
    public void Subscribe<TEvent, THandler>(
        ConsumerConfig? config = null,
        string? topic = null,
        string? groupId = null,
        bool? enableAutoCommit = true) where THandler : IEventHandler<TEvent>
    {
        string eventName = typeof(TEvent).Name;
        if (string.IsNullOrEmpty(topic)) {
            topic = eventName;
        }
        
        string handlerName = typeof(THandler).Name;
        if (string.IsNullOrEmpty(groupId)) {
            groupId = handlerName;
        }

        if (!_consumers.ContainsKey(topic))
        {
            _consumers[topic] = BuildConsumer(topic, groupId, enableAutoCommit, config);
        }

        _consumers[topic].Received += async (sender, args) => 
            await KafkaConsumerOnReceived<TEvent, THandler>(sender, args);
        
        _logger?.LogInformation("Subscribed to {Event}: {Handler}", eventName, handlerName);
    }

    private IKafkaConsumerService BuildConsumer(
        string topic,
        string groupId, 
        bool? enableAutoCommit,
        ConsumerConfig? config)
    {
        IKafkaConsumerService kafkaConsumerService = new KafkaConsumerService(config, _serviceProvider.GetService<ILogger<IKafkaConsumerService>>());
        kafkaConsumerService.Consume(topic, groupId, enableAutoCommit ?? true);
        _logger?.LogInformation("Consume has been started. Topic: {Topic}", topic);
        return kafkaConsumerService;
    }

    private async Task KafkaConsumerOnReceived<TEvent, THandler>(object? _, ReceivedEventDetails e) where THandler : IEventHandler<TEvent>
    {
        TEvent? @event = JsonConvert.DeserializeObject<TEvent>(e.Message);
        THandler eventHandler = ActivatorUtilities.CreateInstance<THandler>(_serviceProvider);

        if (@event != null) {
            await eventHandler.HandleAsync(@event);
        }
    }
    
    public void Dispose() 
    {
        _consumers.ToList().ForEach(x => x.Value.Dispose());
        GC.SuppressFinalize(this);
    }
}