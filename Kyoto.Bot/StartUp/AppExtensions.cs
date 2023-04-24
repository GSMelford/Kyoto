using Confluent.Kafka;
using Kyoto.Bot.KafkaHandlers;
using Kyoto.Bot.KafkaHandlers.GlobalCommandHandlers;
using Kyoto.Bot.StartUp.Settings;
using Kyoto.Infrastructure;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.StartUp;

public static class AppExtensions
{
    public static void SubscribeToEvents(this WebApplication app, AppSettings appSettings)
    {
        var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
        var consumerConfig = new ConsumerConfig{ BootstrapServers = appSettings.KafkaBootstrapServers };
        
        kafkaConsumerFactory.Subscribe<CommandEvent, CommandHandler>(consumerConfig);
        kafkaConsumerFactory.Subscribe<StartCommandEvent, StartCommandHandler>(consumerConfig);
        kafkaConsumerFactory.Subscribe<MessageEvent, MessageHandler>(consumerConfig);
    }

    public static async Task PrepareDatabaseAsync(this IServiceProvider serviceProvider, DatabaseSettings databaseSettings)
    {
        using var scope = serviceProvider.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<IDatabaseContext>();
        await databaseContext.MigrateAsync(databaseSettings.ToConnectionString());
    }
}