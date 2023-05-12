using Kyoto.Database;
using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Settings;

namespace Kyoto.Services.BotClient.Deploy;

public class DeployService : IDeployService
{
    private readonly DatabaseSettings _databaseSettings;
    private readonly IDatabaseContext _databaseContext;
    private readonly IDeployRepository _deployRepository;

    public DeployService(DatabaseSettings databaseSettings, IDatabaseContext databaseContext, IDeployRepository deployRepository)
    {
        _databaseSettings = databaseSettings;
        _databaseContext = databaseContext;
        _deployRepository = deployRepository;
    }

    public async Task DeployAsync(string tenant, bool isNew)
    {
        await _databaseContext.MigrateAsync(_databaseSettings.ToConnectionString(tenant));

        if (isNew)
        {
            await _deployRepository.InitDatabaseAsync();
        }
    }
}