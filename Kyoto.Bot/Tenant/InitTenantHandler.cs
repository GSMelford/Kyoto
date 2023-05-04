using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.Core.Tenant;

public class InitTenantHandler : IKafkaHandler<InitTenantEvent>
{
    private readonly string _bootstrapServers;
    private readonly IKafkaEventSubscriber _kafkaEventSubscriber;

    public InitTenantHandler(string bootstrapServers, IKafkaEventSubscriber kafkaEventSubscriber)
    {
        _bootstrapServers = bootstrapServers;
        _kafkaEventSubscriber = kafkaEventSubscriber;
    }
    
    public async Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        var isAdd = BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));
        
        if (isAdd)
        {
            await _kafkaEventSubscriber.SubscribeAsync(initTenantEvent.TenantKey, _bootstrapServers);
        }
    }
}