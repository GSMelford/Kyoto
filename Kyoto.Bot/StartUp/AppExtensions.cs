using Confluent.Kafka;
using Kyoto.Bot.KafkaHandlers;
using Kyoto.Bot.KafkaHandlers.CommandHandlers;
using Kyoto.Bot.StartUp.Settings;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.StartUp;

public static class AppExtensions
{
    public static void SubscribeToEvents(this WebApplication app, AppSettings appSettings)
    {
        IKafkaConsumerFactory kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
        ConsumerConfig consumerConfig = new ConsumerConfig{BootstrapServers = appSettings.KafkaBootstrapServers};
        kafkaConsumerFactory.Subscribe<CommandEvent, CommandHandler>(consumerConfig);
        kafkaConsumerFactory.Subscribe<StartCommandEvent, StartCommandMessageHandler>(consumerConfig);
    }
}