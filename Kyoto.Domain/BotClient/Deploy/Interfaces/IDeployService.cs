using Kyoto.Domain.Deploy;

namespace Kyoto.Domain.BotClient.Deploy.Interfaces;

public interface IDeployService
{
    Task DeployAsync(InitTenantInfo initTenantInfo);
}