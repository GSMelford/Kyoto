using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.Tenant;

namespace Kyoto.Telegram.Sender.KafkaHandlers;

public class InitSenderTenantHandler : IKafkaHandler<InitTenantEvent>
{
    private readonly IKafkaEventSubscriber _kafkaEventSubscriber;

    public InitSenderTenantHandler(IKafkaEventSubscriber kafkaEventSubscriber)
    {
        _kafkaEventSubscriber = kafkaEventSubscriber;
    }
    
    public async Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        if (BotTenantFactory.Store.AddOrUpdateTenant(BotTenantModel.Create(initTenantEvent.TenantKey, initTenantEvent.Token)))
        {
            await _kafkaEventSubscriber.SubscribeAsync(initTenantEvent.TenantKey);
        }
    }
}