namespace Kyoto.Domain.Tenant;

public class CurrentBotTenant
{
    private static readonly AsyncLocal<BotTenantModel?> _botTenant = new();
    
    public static BotTenantModel? BotTenant
    {
        get => _botTenant.Value;
        set => _botTenant.Value = value;
    }

    public static IDisposable SetBotTenant(BotTenantModel? tenantBotId)
    {
        BotTenant = tenantBotId;
        return new Disposable(() => { BotTenant = null; });
    }
}

public class Disposable : IDisposable
{
    private Action? _dispose;

    public Disposable(Action? dispose)
    {
        ArgumentNullException.ThrowIfNull(dispose);
        _dispose = dispose;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        _dispose?.Invoke();
        _dispose = null;
    }
}