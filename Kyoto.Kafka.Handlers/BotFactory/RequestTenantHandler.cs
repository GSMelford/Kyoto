using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.Tenant;
using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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

    public async Task HandleAsync(RequestTenantEvent requestTenantEvent)
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