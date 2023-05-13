using Kyoto.Database.CommonRepositories.Deploy;

namespace Kyoto.Database.BotClient.Repositories.Deploy;

public class DeployBotClientRepository : BaseDeployRepository
{
    public DeployBotClientRepository(IDatabaseContext databaseContext) : base(databaseContext)
    {
    }
}