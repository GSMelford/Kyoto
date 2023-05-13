using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kyoto.Kafka.Handlers.BotFactory;

public class RequestTenantHandler : IKafkaHandler<RequestTenantEvent>
{
    private readonly ILogger<IKafkaHandler<RequestTenantEvent>> _logger;
    private readonly ITenantService _tenantService;

    public RequestTenantHandler(ILogger<IKafkaHandler<RequestTenantEvent>> logger, ITenantService tenantService)
    {
        _logger = logger;
        _tenantService = tenantService;
    }

    public async Task HandleAsync(RequestTenantEvent @event)
    {
        await _tenantService.InitMainBotTenantAsync();

        try
        {
            await _tenantService.InitBotTenantsFromDatabaseAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Database not ready!");
        }
    }
}