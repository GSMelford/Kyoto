namespace Kyoto.Domain.Menu;

public class MenuPanel
{
    public string Name { get; private set; }
    public List<MenuButton> MenuButtons { get; private set; }

    private MenuPanel(string name, List<MenuButton> menuButtons)
    {
        Name = name;
        MenuButtons = menuButtons;
    }

    public static MenuPanel Create(string name, List<MenuButton> menuButtons)
    {
        return new MenuPanel(name, menuButtons);
    }
}