using Confluent.Kafka;
using Kyoto.Bot.KafkaHandlers.GlobalCommandHandlers;
using Kyoto.Bot.StartUp.Settings;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers;

public class InitTenantHandler : IKafkaHandler<InitTenantEvent>
{
    private readonly AppSettings _appSettings;
    private readonly IKafkaConsumerFactory _kafkaConsumerFactory;

    public InitTenantHandler(AppSettings appSettings, IKafkaConsumerFactory kafkaConsumerFactory)
    {
        _appSettings = appSettings;
        _kafkaConsumerFactory = kafkaConsumerFactory;
    }
    
    public async Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        bool isAdd = BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));
        
        if (isAdd)
        {
            var consumerConfig = new ConsumerConfig { BootstrapServers = _appSettings.KafkaBootstrapServers };
        
            await _kafkaConsumerFactory.SubscribeAsync<CommandEvent, CommandHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
            await _kafkaConsumerFactory.SubscribeAsync<StartCommandEvent, StartCommandHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
            await _kafkaConsumerFactory.SubscribeAsync<MessageEvent, MessageHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
            await _kafkaConsumerFactory.SubscribeAsync<CallbackQueryEvent, CallbackQueryHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
        }
    }
}