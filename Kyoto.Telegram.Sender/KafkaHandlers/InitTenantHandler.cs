using Confluent.Kafka;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Telegram.Sender.KafkaHandlers;

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
        _kafkaConsumerFactory.Subscribe<RequestEvent, RequestHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
        
        BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));
        
        return Task.CompletedTask;
    }
}