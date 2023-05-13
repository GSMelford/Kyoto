using Kyoto.Database;
using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.Deploy;
using Kyoto.Settings;

namespace Kyoto.Services.Deploy;

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

    public async Task DeployAsync(InitTenantInfo initTenantInfo)
    {
        await _databaseContext.MigrateAsync(_databaseSettings.ToConnectionString(initTenantInfo.TenantKey));
        await _deployRepository.InitDatabaseAsync(initTenantInfo);
    }
}