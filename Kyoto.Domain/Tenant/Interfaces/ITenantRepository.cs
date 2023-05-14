namespace Kyoto.Domain.Tenant.Interfaces;

public interface ITenantRepository
{
    IAsyncEnumerable<BotTenant> GetAllActiveTenantsAsync();
    Task<BotTenant> GetBotTenantAsync(long externalId, string name);
}