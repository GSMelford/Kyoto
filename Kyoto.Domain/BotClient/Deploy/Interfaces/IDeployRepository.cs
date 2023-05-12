namespace Kyoto.Domain.BotClient.Deploy.Interfaces;

public interface IDeployRepository
{
    Task InitDatabaseAsync();
}