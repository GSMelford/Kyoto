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

    public async Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        bool isAdd = BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));
        
        if (isAdd) 
        {
            var consumerConfig = new ConsumerConfig { BootstrapServers = _appSettings.KafkaBootstrapServers };
            await _kafkaConsumerFactory.SubscribeAsync<RequestEvent, RequestHandler>(consumerConfig, topicPrefix: initTenantEvent.TenantKey);
        }
    }
}