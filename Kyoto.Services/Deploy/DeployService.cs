using Kyoto.Database;
using Kyoto.Domain.BotClient.Deploy.Interfaces;
using Kyoto.Domain.Deploy;
using Kyoto.Settings;
using Microsoft.Extensions.Logging;

namespace Kyoto.Services.Deploy;

public class DeployService : IDeployService
{
    private readonly ILogger<IDeployService> _logger;
    private readonly DatabaseSettings _databaseSettings;
    private readonly IDatabaseContext _databaseContext;
    private readonly IDeployRepository _deployRepository;

    public DeployService(
        ILogger<IDeployService> logger,
        DatabaseSettings databaseSettings,
        IDatabaseContext databaseContext,
        IDeployRepository deployRepository)
    {
        _logger = logger;
        _databaseSettings = databaseSettings;
        _databaseContext = databaseContext;
        _deployRepository = deployRepository;
    }

    public async Task DeployAsync(InitTenantInfo initTenantInfo)
    {
        await _databaseContext.MigrateAsync(_databaseSettings.ToConnectionString(initTenantInfo.TenantKey));
        try
        {
            await _deployRepository.InitDatabaseAsync(initTenantInfo);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Init Database error! Tenant: {TenantKey}", initTenantInfo.TenantKey);
        }
    }
}