using Confluent.Kafka;
using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers.BotFactory;
using Kyoto.Kafka.Interfaces;
using Kyoto.Settings;

namespace Kyoto.Bot.Client.KafkaHandlers;

public class InitTenantHandler : IKafkaHandler<InitTenantEvent>
{
    private readonly KafkaSettings _kafkaSettings;
    private readonly IKafkaConsumerFactory _kafkaConsumerFactory;
    private readonly IDeployService _deployService;
    private readonly IKafkaProducer<string> _kafkaProducer;

    public InitTenantHandler(
        KafkaSettings kafkaSettings,
        IKafkaConsumerFactory kafkaConsumerFactory, 
        IDeployService deployService, 
        IKafkaProducer<string> kafkaProducer)
    {
        _kafkaSettings = kafkaSettings;
        _kafkaConsumerFactory = kafkaConsumerFactory;
        _deployService = deployService;
        _kafkaProducer = kafkaProducer;
    }

    public async Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        if (initTenantEvent.IsFactory) {
            return;
        }
        
        var added = BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(initTenantEvent.TenantKey, initTenantEvent.Token));

        if (added)
        {
            var consumerConfig = new ConsumerConfig { BootstrapServers = _kafkaSettings.BootstrapServers };
        
            await _kafkaConsumerFactory.SubscribeAsync<MessageEvent, MessageHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
            await _kafkaConsumerFactory.SubscribeAsync<CallbackQueryEvent, CallbackQueryHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
            await _kafkaConsumerFactory.SubscribeAsync<CommandEvent, CommandHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);

            await _deployService.DeployAsync(initTenantEvent.TenantKey, initTenantEvent.IsNew);
            
            await _kafkaProducer.ProduceAsync(
                new DeployStatusEvent(Session.CreatePersonalNew(initTenantEvent.TenantKey, initTenantEvent.OwnerExternalUserId)), string.Empty);
        }
    }
}