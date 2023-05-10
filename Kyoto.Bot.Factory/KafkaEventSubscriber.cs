using Kyoto.Domain.Settings;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers.BotFactory;
using Kyoto.Kafka.Handlers.BotFactory.GlobalCommandHandlers;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.Tenant;

namespace Kyoto.Bot;

public class KafkaEventSubscriber : BaseKafkaEventSubscriber
{
    public KafkaEventSubscriber(IKafkaConsumerFactory kafkaConsumerFactory, KafkaSettings kafkaSettings) 
        : base(kafkaConsumerFactory, kafkaSettings)
    {
    }

    public override async Task SubscribeAsync(string tenantKey)
    {
        await KafkaConsumerFactory.SubscribeAsync<CallbackQueryEvent, CallbackQueryHandler>(ConsumerConfig, topicPrefix: tenantKey);
        await KafkaConsumerFactory.SubscribeAsync<CommandEvent, CommandHandler>(ConsumerConfig, topicPrefix: tenantKey);
        await KafkaConsumerFactory.SubscribeAsync<MessageEvent, MessageHandler>(ConsumerConfig, topicPrefix: tenantKey);
        await KafkaConsumerFactory.SubscribeAsync<StartCommandEvent, StartCommandHandler>(ConsumerConfig, topicPrefix: tenantKey);
    }
}