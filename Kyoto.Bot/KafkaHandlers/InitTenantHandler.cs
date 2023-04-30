using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers;

public class InitTenantHandler : IKafkaHandler<InitTenantEvent>
{
    public Task HandleAsync(InitTenantEvent initTenantEvent)
    {
        BotTenantFactory.Store.AddOrUpdateTenant(
            BotTenantModel.Create(
                initTenantEvent.TenantKey,
                initTenantEvent.Token));
        
        return Task.CompletedTask;
    }
}