namespace Kyoto.Domain.Tenant;

public class BotTenantModel
{
    public string TenantKey { get; private set; }
    public string Token { get; private set; }
    public bool IsFactory { get; private set; }

    private BotTenantModel(string tenantKey, string token, bool isFactory)
    {
        TenantKey = tenantKey;
        Token = token;
        IsFactory = isFactory;
    }

    public static BotTenantModel Create(string tenantKey, string token, bool isFactory)
    {
        return new BotTenantModel(tenantKey, token, isFactory);
    }
    
    public static BotTenantModel Create(string tenantKey)
    {
        return new BotTenantModel(tenantKey, string.Empty, false);
    }
}