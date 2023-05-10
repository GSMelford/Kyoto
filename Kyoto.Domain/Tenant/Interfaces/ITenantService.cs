namespace Kyoto.Domain.Tenant.Interfaces;

public interface ITenantService
{
    Task InitMainBotTenantAsync();
    Task InitBotTenantsFromDatabaseAsync();
}