using Confluent.Kafka;
using Kyoto.Dal;
using Kyoto.Domain.Settings;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Handlers.BotFactory;
using Kyoto.Kafka.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Extensions;

public static class AppExtensions
{
    public static async Task SubscribeToEventsAsync(this IServiceProvider serviceProvider, KafkaSettings kafkaSettings)
    {
        var kafkaConsumerFactory = serviceProvider.GetRequiredService<IKafkaConsumerFactory>();
        var consumerConfig = new ConsumerConfig{ BootstrapServers = kafkaSettings.BootstrapServers };
        
        await kafkaConsumerFactory.SubscribeAsync<RequestTenantEvent, RequestTenantHandler>(consumerConfig);
        await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitTenantHandler>(consumerConfig, groupId: $"{nameof(InitTenantHandler)}-Factory");
    }

    public static async Task PrepareDatabaseAsync(this IServiceProvider serviceProvider, DatabaseSettings databaseSettings)
    {
        using var scope = serviceProvider.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<IDatabaseContext>();
        await databaseContext.MigrateAsync(databaseSettings.ToConnectionString());
    }
    
    public static async Task InitBotTenantsAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var kafkaProducer = scope.ServiceProvider.GetRequiredService<IKafkaProducer<string>>();
        await kafkaProducer.ProduceAsync(new RequestTenantEvent { SessionId = Guid.NewGuid() }, string.Empty);
    }
}