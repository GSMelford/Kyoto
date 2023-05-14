using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Handlers.BotClient;
using Kyoto.Kafka.Handlers.BotFactory;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.Tenant;
using Kyoto.Settings;

namespace Kyoto.Bot.Client;

public class KafkaEventSubscriber : BaseKafkaEventSubscriber
{
    public KafkaEventSubscriber(
        IKafkaConsumerFactory kafkaConsumerFactory, 
        KafkaSettings kafkaSettings) : base(kafkaConsumerFactory, kafkaSettings)
    {
    }

    public override async Task SubscribeAsync(string tenantKey)
    {
        await KafkaConsumerFactory.SubscribeAsync<MessageEvent, MessageHandler>(ConsumerConfig, topicPrefix: tenantKey);
        await KafkaConsumerFactory.SubscribeAsync<CallbackQueryEvent, CallbackQueryHandler>(ConsumerConfig, topicPrefix: tenantKey);
        await KafkaConsumerFactory.SubscribeAsync<CommandEvent, CommandHandler>(ConsumerConfig, topicPrefix: tenantKey);
        await KafkaConsumerFactory.SubscribeAsync<TemplateMessageEvent, TemplateMessageHandler>(ConsumerConfig, topicPrefix: tenantKey);
    }
}