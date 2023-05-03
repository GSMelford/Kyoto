using System.Collections.Concurrent;

namespace Kyoto.Domain.Tenant;

public class BotTenantFactory : IDisposable
{
    private readonly ConcurrentDictionary<string, BotTenantModel> _botTenantModels;
    public static BotTenantFactory Store { get; } = new();

    public BotTenantFactory()
    {
        _botTenantModels = new ConcurrentDictionary<string, BotTenantModel>();
    }
    
    public void AddOrUpdateTenant(BotTenantModel botTenantModel)
    {
        _botTenantModels.AddOrUpdate(botTenantModel.TenantKey, botTenantModel, (_, model) => model);
    }

    public BotTenantModel Get(string tenantKey)
    {
        return _botTenantModels[tenantKey];
    }

    public bool IsExist(string tenantKey)
    {
        return _botTenantModels.ContainsKey(tenantKey);
    }
    
    public void Dispose()
    {
        _botTenantModels.Clear();
    }
}