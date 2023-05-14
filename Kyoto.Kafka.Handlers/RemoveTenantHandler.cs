using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Kafka.Handlers;

public class RemoveTenantHandler : IKafkaHandler<RemoveTenantEvent>
{
    public Task HandleAsync(RemoveTenantEvent removeTenantEvent)
    {
        BotTenantFactory.Store.RemoveTenant(removeTenantEvent.TenantKey);
        return Task.CompletedTask;
    }
}