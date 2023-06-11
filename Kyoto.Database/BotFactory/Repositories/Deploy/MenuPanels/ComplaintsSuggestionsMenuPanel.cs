using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using MenuPanelDal = Kyoto.Database.CommonModels.MenuPanel;
using MenuButtonDal = Kyoto.Database.CommonModels.MenuButton;

namespace Kyoto.Database.BotFactory.Repositories.Deploy.MenuPanels;

public class ComplaintsSuggestionsMenuPanel
{
    public static MenuPanelDal Get(string backMenuPanelName)
    {
        return new MenuPanelDal
        {
            Name = MenuPanelConstants.ComplaintsSuggestionsMenuPanel,
            Buttons = new List<MenuButtonDal>
            {
                new ()
                {
                    Text = "😨 Скарги",
                    IsCommand = true,
                    Code = CommandCodes.ComplaintsSuggestions.Complaints,
                    Index = 0,
                    Line = 0,
                    IsEnable = true
                },
                new ()
                {
                    Text = "❤️ Залишити відгук",
                    IsCommand = true,
                    Code = CommandCodes.ComplaintsSuggestions.Suggestions,
                    Index = 1,
                    Line = 0,
                    IsEnable = true
                },
                new ()
                {
                    Text = MenuPanelConstants.BuildBackText(backMenuPanelName),
                    Code = MenuPanelConstants.MenuPanelCode,
                    Index = 0,
                    Line = 1,
                    IsEnable = true
                }
            }
        };
    }
}