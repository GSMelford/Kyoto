namespace Kyoto.Domain.Menu.Interfaces;

public interface IMenuRepository
{
    Task<bool> IsMenuPanelButtonAsync(string name);
    Task<MenuPanel> GetMenuPanelAsync(string menuPanelName);
}