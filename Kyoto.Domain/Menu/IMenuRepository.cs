namespace Kyoto.Domain.Menu;

public interface IMenuRepository
{
    Task CreateOrUpdateAsync(long externalId, string newMenuPanel);
    Task<string?> GetPreviousMenuPanelAsync(long externalId);
}