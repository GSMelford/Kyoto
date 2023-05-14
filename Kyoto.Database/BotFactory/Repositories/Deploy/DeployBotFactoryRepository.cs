using Kyoto.Database.BotFactory.Repositories.Deploy.MenuPanels;
using Kyoto.Database.CommonRepositories.Deploy;

namespace Kyoto.Database.BotFactory.Repositories.Deploy;

public class DeployBotFactoryRepository : BaseDeployRepository
{
    public DeployBotFactoryRepository(IDatabaseContext databaseContext) : base(databaseContext)
    {
    }

    protected override async Task InitMenuAsync()
    {
        var homeMenuPanel = HomePanel.Get();
        await DatabaseContext.SaveAsync(homeMenuPanel);

        var botManagementMenuPanel = BotManagementPanel.Get(homeMenuPanel.Name);
        await DatabaseContext.SaveAsync(botManagementMenuPanel);
        
        var botFeaturesMenuPanel = BotFeaturesMenuPanel.Get(homeMenuPanel.Name);
        await DatabaseContext.SaveAsync(botFeaturesMenuPanel);
        
        var complaintsSuggestionsMenuPanel = ComplaintsSuggestionsMenuPanel.Get(homeMenuPanel.Name);
        await DatabaseContext.SaveAsync(complaintsSuggestionsMenuPanel);
    }
}