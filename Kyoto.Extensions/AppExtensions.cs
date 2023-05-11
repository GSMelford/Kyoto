using Confluent.Kafka;
using Kyoto.Dal;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Handlers.BotFactory;
using Kyoto.Kafka.Handlers.BotFactory.GlobalCommandHandlers;
using Kyoto.Kafka.Interfaces;
using Kyoto.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Extensions;

public static class AppExtensions
{
    public static async Task SubscribeToEventsAsync(this IServiceProvider serviceProvider, KafkaSettings kafkaSettings, string tenantKey)
    {
        var kafkaConsumerFactory = serviceProvider.GetRequiredService<IKafkaConsumerFactory>();
        var consumerConfig = new ConsumerConfig{ BootstrapServers = kafkaSettings.BootstrapServers };
        
        await kafkaConsumerFactory.SubscribeAsync<RequestTenantEvent, RequestTenantHandler>(consumerConfig);
        await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitTenantHandler>(consumerConfig, groupId: $"{nameof(InitTenantHandler)}-Factory");
        await kafkaConsumerFactory.SubscribeAsync<MessageEvent, MessageHandler>(consumerConfig, topicPrefix: tenantKey);
        await kafkaConsumerFactory.SubscribeAsync<CallbackQueryEvent, CallbackQueryHandler>(consumerConfig, topicPrefix: tenantKey);
        await kafkaConsumerFactory.SubscribeAsync<CommandEvent, CommandHandler>(consumerConfig, topicPrefix: tenantKey);
        await kafkaConsumerFactory.SubscribeAsync<StartCommandEvent, StartCommandHandler>(consumerConfig, topicPrefix: tenantKey);
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