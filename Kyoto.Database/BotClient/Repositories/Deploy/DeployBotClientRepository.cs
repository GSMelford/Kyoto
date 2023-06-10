using Kyoto.Database.BotClient.Repositories.Deploy.MenuPanels;
using Kyoto.Database.Repositories.Deploy;

namespace Kyoto.Database.BotClient.Repositories.Deploy;

public class DeployBotClientRepository : BaseDeployRepository
{
    public DeployBotClientRepository(IDatabaseContext databaseContext) : base(databaseContext)
    {
    }
    
    protected override Task InitMenuAsync()
    {
        var homeMenuPanel = HomePanel.Get();
        return DatabaseContext.SaveAsync(homeMenuPanel);
    }
}