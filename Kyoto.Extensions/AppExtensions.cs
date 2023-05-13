using Confluent.Kafka;
using Kyoto.Database;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Handlers.BotFactory;
using Kyoto.Kafka.Interfaces;
using Kyoto.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Extensions;

public static class AppExtensions
{
    public static async Task SubscribeToRequestTenantEventAsync(this IServiceProvider serviceProvider, KafkaSettings kafkaSettings)
    {
        var kafkaConsumerFactory = serviceProvider.GetRequiredService<IKafkaConsumerFactory>();
        var consumerConfig = new ConsumerConfig{ BootstrapServers = kafkaSettings.BootstrapServers };
        
        await kafkaConsumerFactory.SubscribeAsync<RequestTenantEvent, RequestTenantHandler>(consumerConfig);
    }

    public static async Task SendRequestBotTenantsAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var kafkaProducer = scope.ServiceProvider.GetRequiredService<IKafkaProducer<string>>();
        await kafkaProducer.ProduceAsync(new RequestTenantEvent { SessionId = Guid.NewGuid() }, string.Empty);
    }
}