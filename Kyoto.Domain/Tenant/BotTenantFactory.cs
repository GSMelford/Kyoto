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
    
    public bool AddOrUpdateTenant(BotTenantModel botTenantModel)
    {
        if (_botTenantModels.ContainsKey(botTenantModel.TenantKey)) return false;
        _botTenantModels.AddOrUpdate(botTenantModel.TenantKey, botTenantModel, (_, model) => model);
        return true;
    }
    
    public void RemoveTenant(string tenantKey)
    {
        if (!_botTenantModels.ContainsKey(tenantKey)) return;
        _botTenantModels.Remove(tenantKey, out _);
    }

    public BotTenantModel Get(string tenantKey)
    {
        return _botTenantModels[tenantKey];
    }

    public void Dispose()
    {
        _botTenantModels.Clear();
    }
}