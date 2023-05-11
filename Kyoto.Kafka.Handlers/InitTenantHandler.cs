using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.Tenant;

namespace Kyoto.Kafka.Handlers;

public class InitTenantHandler : IKafkaHandler<InitTenantEvent>
{
    private readonly IKafkaEventSubscriber? _kafkaEventSubscriber;

    public InitTenantHandler(IKafkaEventSubscriber? kafkaEventSubscriber = null)
    {
        _kafkaEventSubscriber = kafkaEventSubscriber;
    }
    
    public async Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        var isAdd = BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));
        
        if (isAdd && _kafkaEventSubscriber is not null)
        {
            await _kafkaEventSubscriber.SubscribeAsync(initTenantEvent.TenantKey);
        }
    }
}