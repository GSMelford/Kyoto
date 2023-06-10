using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using MenuPanelDal = Kyoto.Database.CommonModels.MenuPanel;
using MenuButtonDal = Kyoto.Database.CommonModels.MenuButton;

namespace Kyoto.Database.BotFactory.Repositories.Deploy.MenuPanels;

public abstract class BotManagementPanel
{
    public static MenuPanelDal Get(string backMenuPanelName)
    {
        return new MenuPanelDal
        {
            Name = MenuPanelConstants.BotManagementMenuPanel,
            Buttons = new List<MenuButtonDal>
            {
                new ()
                {
                    Text = "🏗 Зареєструвати бота",
                    IsCommand = true,
                    Code = CommandCodes.BotManagement.BotRegistration,
                    Index = 0,
                    Line = 0,
                    IsEnable = true
                },
                new ()
                {
                    Text = "🚀 Запустити бота",
                    IsCommand = true,
                    Code = CommandCodes.BotManagement.DeployBot,
                    Index = 0,
                    Line = 1,
                    IsEnable = true
                },
                new ()
                {
                    Text = "😴 Вимкнути бота",
                    IsCommand = true,
                    Code = CommandCodes.BotManagement.DisableBot,
                    Index = 1,
                    Line = 1,
                    IsEnable = true
                },
                new ()
                {
                    Text = "❌ Видалити бота",
                    IsCommand = true,
                    Code = CommandCodes.BotManagement.DeleteBot,
                    Index = 2,
                    Line = 1,
                    IsEnable = true
                },
                new ()
                {
                    Text = MenuPanelConstants.BuildBackText(backMenuPanelName),
                    Code = MenuPanelConstants.MenuPanelCode,
                    Index = 0,
                    Line = 2,
                    IsEnable = true
                }
            }
        };
    }
}