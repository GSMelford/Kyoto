using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Kafka.Handlers.BotFactory;

public class RequestTenantHandler : IKafkaHandler<RequestTenantEvent>
{
    private readonly ITenantService _tenantService;

    public RequestTenantHandler(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    public async Task HandleAsync(RequestTenantEvent @event)
    {
        await _tenantService.InitMainBotTenantAsync();
        await _tenantService.InitBotTenantsFromDatabaseAsync();
    }
}