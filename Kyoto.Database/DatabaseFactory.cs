using Kyoto.Database.BotClient;
using Kyoto.Settings;

namespace Kyoto.Database;

public class DatabaseFactory : IDatabaseFactory, IAsyncDisposable
{
    private readonly DatabaseSettings _databaseSettings;
    private IDatabaseContext _databaseContext = null!;
    
    public DatabaseFactory(DatabaseSettings databaseSettings)
    {
        _databaseSettings = databaseSettings;
    }

    public IDatabaseContext GetContext(string tenantKey)
    {
        _databaseContext = new DatabaseBotClientContext(_databaseSettings.ToConnectionString(tenantKey));
        return _databaseContext;
    }

    public async ValueTask DisposeAsync()
    {
        await _databaseContext.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}