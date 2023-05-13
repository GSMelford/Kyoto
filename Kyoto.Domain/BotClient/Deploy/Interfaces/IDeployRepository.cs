using Kyoto.Domain.Deploy;

namespace Kyoto.Domain.BotClient.Deploy.Interfaces;

public interface IDeployRepository
{
    Task InitDatabaseAsync(InitTenantInfo initTenantInfo);
}