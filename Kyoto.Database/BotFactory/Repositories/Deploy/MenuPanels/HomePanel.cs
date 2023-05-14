using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using MenuPanelDal = Kyoto.Database.CommonModels.MenuPanel;
using MenuButtonDal = Kyoto.Database.CommonModels.MenuButton;

namespace Kyoto.Database.BotFactory.Repositories.Deploy.MenuPanels;

public static class HomePanel
{
    public static MenuPanelDal Get()
    {
        return new MenuPanelDal
        {
            Name = MenuPanelConstants.HomeMenuPanel,
            Buttons = new List<MenuButtonDal>
            {
                new ()
                {
                    Text = MenuPanelConstants.BotManagementMenuPanel,
                    Code = MenuPanelConstants.MenuPanelCode,
                    Index = 0,
                    Line = 0,
                    IsEnable = true
                },
                new ()
                {
                    Text = MenuPanelConstants.BotFeaturesMenuPanel,
                    Code = MenuPanelConstants.MenuPanelCode,
                    Index = 0,
                    Line = 1,
                    IsEnable = false
                },
                new ()
                {
                    Text = "📄 Generate QR-Code",
                    Code = CommandCodes.GenerateQrCode,
                    IsCommand = true,
                    Index = 1,
                    Line = 1,
                    IsEnable = false
                },
                new ()
                {
                    Text = "🍙 About the bot",
                    Code = CommandCodes.AboutBot,
                    IsCommand = true,
                    Index = 0,
                    Line = 2,
                    IsEnable = true
                },
                new ()
                {
                    Text = MenuPanelConstants.ComplaintsSuggestionsMenuPanel,
                    Code = MenuPanelConstants.MenuPanelCode,
                    Index = 1,
                    Line = 2,
                    IsEnable = true
                }
            }
        };
    }
}