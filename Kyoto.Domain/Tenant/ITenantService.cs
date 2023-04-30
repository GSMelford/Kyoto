namespace Kyoto.Domain.Tenant;

public interface ITenantService
{
    Task InitMainBotTenantAsync();
    Task InitBotTenantsFromDatabaseAsync();
}