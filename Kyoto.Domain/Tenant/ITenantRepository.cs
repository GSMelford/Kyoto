namespace Kyoto.Domain.Tenant;

public interface ITenantRepository
{
    IAsyncEnumerable<BotTenant> GetAllTenantsAsync();
    Task<BotTenant> GetBotTenantAsync(long externalId, string name);
}