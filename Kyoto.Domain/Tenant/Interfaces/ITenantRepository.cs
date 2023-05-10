namespace Kyoto.Domain.Tenant.Interfaces;

public interface ITenantRepository
{
    IAsyncEnumerable<BotTenant> GetAllTenantsAsync();
    Task<BotTenant> GetBotTenantAsync(long externalId, string name);
}