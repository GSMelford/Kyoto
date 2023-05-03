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
    
    public Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        if (BotTenantFactory.Store.IsExist(initTenantEvent.TenantKey)) {
            return Task.CompletedTask;
        }
        
        var consumerConfig = new ConsumerConfig { BootstrapServers = _appSettings.KafkaBootstrapServers };
        
        _kafkaConsumerFactory.Subscribe<CommandEvent, CommandHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
        _kafkaConsumerFactory.Subscribe<StartCommandEvent, StartCommandHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
        _kafkaConsumerFactory.Subscribe<MessageEvent, MessageHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
        _kafkaConsumerFactory.Subscribe<CallbackQueryEvent, CallbackQueryHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
        
        BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));
        
        return Task.CompletedTask;
    }
}