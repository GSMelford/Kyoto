namespace Kyoto.Domain.Tenant;

public class BotTenant
{
    public string TenantKey { get; }
    public string Token { get; }

    private BotTenant(string tenantKey, string token)
    {
        TenantKey = tenantKey;
        Token = token;
    }

    public static BotTenant Create(string tenantKey, string token)
    {
        return new BotTenant(tenantKey, token);
    }
}