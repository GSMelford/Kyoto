namespace Kyoto.Domain.Menu.Interfaces;

public interface IMenuRepository
{
    Task<bool> IsMenuPanelAsync(string name);
    Task<MenuPanel> GetMenuPanelAsync(string menuPanelName);
    Task<bool> IsMenuButtonAsync(string menuButtonText);
    Task<string> GetMenuButtonCodeAsync(string menuButtonText);
    Task EnableMenuAsync(string menuButtonText);
}