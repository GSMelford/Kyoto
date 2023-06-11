namespace Kyoto.Domain.Menu.Interfaces;

public interface IMenuRepository
{
    Task<bool> IsMenuPanelAsync(string name);
    Task<MenuPanel> GetMenuPanelAsync(string menuPanelName);
    Task<bool> IsMenuButtonAsync(string menuButtonText);
    Task<string> GetMenuButtonCodeAsync(string menuButtonText);
    Task SetMenuButtonStatusAsync(string menuButtonText, bool isEnable = true);
    Task AddAccessToWatchButtonAsync(long externalId, string menuButtonText);
    Task RemoveAccessToWatchButtonAsync(long externalId, string menuButtonText);
    Task<bool> IsAccessToWatchExistAsync(long externalId, Guid menuButtonId);
}