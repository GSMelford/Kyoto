namespace Kyoto.Domain.BotClient.Deploy.Interfaces;

public interface IDeployService
{
    Task DeployAsync(string tenant, bool isNew);
}