using Kyoto.Database.CommonModels;
using Kyoto.Database.CommonRepositories.Deploy;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Deploy;
using Kyoto.Domain.Menu;
using Microsoft.EntityFrameworkCore;
using MenuButton = Kyoto.Database.CommonModels.MenuButton;
using MenuPanel = Kyoto.Database.CommonModels.MenuPanel;

namespace Kyoto.Database.BotFactory.Repositories.Deploy;

public class DeployBotFactoryRepository : BaseDeployRepository
{
    

    public DeployBotFactoryRepository(IDatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public override async Task InitDatabaseAsync(InitTenantInfo initTenantInfo)
    {
        var status = await DatabaseContext.Set<SystemStatus>().FirstOrDefaultAsync(x=>x.Name == "Database");
        if (status?.Status == "Initialized") {
            return;
        }
        
        await base.InitDatabaseAsync(initTenantInfo);
        await InitMenuAsync();
        
        await DatabaseContext.SaveAsync(new SystemStatus
        {
            Name = "Database",
            Status = "Initialized"
        });
    }
    
    private async Task InitMenuAsync()
    {
        var homeMenu = new MenuPanel { Name = MenuPanelConstants.HomeMenuPanel };
        var botManagementMenuPanel = BuildBotManagementMenuPanel(homeMenu);
        
        var homeMenuButtons = new List<MenuButton>
        {
            new ()
            {
                Text = MenuPanelConstants.BotManagementMenuPanelName,
                Code = MenuPanelConstants.MenuPanelCode,
                MenuPanel = botManagementMenuPanel,
                IsEnable = true
            }
        };

        homeMenu.Buttons = homeMenuButtons;
        await DatabaseContext.SaveAsync(botManagementMenuPanel);
        await DatabaseContext.SaveAsync(homeMenu);
    }
    
    private static MenuPanel BuildBotManagementMenuPanel(MenuPanel homePanel)
    {
        var homeMenuButtons = new List<MenuButton>
        {
            new ()
            {
                Text = "🏗 Register a new bot",
                IsCommand = true,
                Code = CommandCodes.BotRegistration,
                Index = 0,
                Line = 0,
                IsEnable = true
            },
            new ()
            {
                Text = "🚀 Deploy bot",
                IsCommand = true,
                Code = CommandCodes.DeleteBot,
                Index = 0,
                Line = 1,
                IsEnable = true
            },
            new ()
            {
                Text = "😴 Disable bot",
                IsCommand = true,
                Code = CommandCodes.DisableBot,
                Index = 1,
                Line = 1,
                IsEnable = true
            },
            new ()
            {
                Text = "❌ Delete bot",
                IsCommand = true,
                Code = CommandCodes.DeleteBot,
                Index = 2,
                Line = 1,
                IsEnable = true
            },
            new ()
            {
                Text = $"{MenuPanelConstants.Back}{homePanel.Name}",
                Code = MenuPanelConstants.MenuPanelCode,
                Index = 0,
                Line = 2,
                IsEnable = true
            }
        };

        return new MenuPanel
        {
            Name = MenuPanelConstants.BotManagementMenuPanelName,
            Buttons = homeMenuButtons
        };
    }
}