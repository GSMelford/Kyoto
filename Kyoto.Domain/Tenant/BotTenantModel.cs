namespace Kyoto.Domain.Tenant;

public class BotTenantModel
{
    public string TenantKey { get; private set; }
    public string Token { get; private set; }

    private BotTenantModel(string tenantKey, string token)
    {
        TenantKey = tenantKey;
        Token = token;
    }

    public static BotTenantModel Create(string tenantKey, string token)
    {
        return new BotTenantModel(tenantKey, token);
    }
}