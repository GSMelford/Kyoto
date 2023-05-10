namespace Kyoto.Domain.BotFactory.Menu.Interfaces;

public interface IMenuRepository
{
    Task CreateOrUpdateAsync(long externalId, string newMenuPanel);
    Task<string?> GetPreviousMenuPanelAsync(long externalId);
}