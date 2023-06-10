using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using MenuPanelDal = Kyoto.Database.CommonModels.MenuPanel;
using MenuButtonDal = Kyoto.Database.CommonModels.MenuButton;

namespace Kyoto.Database.BotClient.Repositories.Deploy.MenuPanels;

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
                    Text = MenuPanelConstants.Client.SendFeedback,
                    Code = CommandCodes.Client.SendFeedback,
                    Index = 0,
                    Line = 0,
                    IsEnable = true,
                    IsCommand = true
                },
                new ()
                {
                    Text = MenuPanelConstants.Client.PreOrderService,
                    Code = CommandCodes.Client.PreOrderService,
                    Index = 0,
                    Line = 1,
                    IsEnable = true,
                    IsCommand = true
                },
                new ()
                {
                    Text = MenuPanelConstants.Client.GetFaq,
                    Code = CommandCodes.Client.GetFaq,
                    Index = 0,
                    Line = 2,
                    IsEnable = true,
                    IsCommand = true
                }
            }
        };
    }
}