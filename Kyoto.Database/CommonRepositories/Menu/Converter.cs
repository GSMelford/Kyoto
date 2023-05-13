using Kyoto.Domain.Menu;

namespace Kyoto.Database.CommonRepositories.Menu;

public static class Converter
{
    public static MenuPanel ToDomain(this CommonModels.MenuPanel menuPanel)
    {
        return MenuPanel.Create(menuPanel.Name, menuPanel.Buttons.Select(ToDomain).ToList());
    }

    public static MenuButton ToDomain(this CommonModels.MenuButton menuButton)
    {
        return MenuButton.Create(
            menuButton.Id, 
            menuButton.Text,
            menuButton.Code,
            menuButton.MenuPanelId,
            menuButton.IsCommand,
            menuButton.IsEnable,
            menuButton.Index,
            menuButton.Line);
    }
}