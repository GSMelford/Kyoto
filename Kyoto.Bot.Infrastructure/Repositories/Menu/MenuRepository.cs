using Kyoto.Bot.Core.Database;
using Kyoto.Domain.Menu;
using Kyoto.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Infrastructure.Repositories.Menu;

public class MenuRepository : IMenuRepository
{
    private readonly IDatabaseContext _databaseContext;

    public MenuRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task CreateOrUpdateAsync(long externalId, string newMenuPanel)
    {
        var menu = await GetAsync(externalId);

        if (menu is null)
        {
            menu = new MenuPanel
            {
                PreviousMenuItem = null,
                ExternalUser = await _databaseContext.Set<ExternalUser>().FirstAsync(x => x.PrivateId == externalId),
                CurrentMenuItem = newMenuPanel
            };
            
            await _databaseContext.AddAsync(menu);
        }
        else
        {
            string tempMenuPanel = menu.CurrentMenuItem;
            menu.PreviousMenuItem = tempMenuPanel;
            menu.CurrentMenuItem = newMenuPanel;
            _databaseContext.Update(menu);
        }

        await _databaseContext.SaveChangesAsync();
    }

    public async Task<string?> GetPreviousMenuPanelAsync(long externalId)
    {
        var menu = await GetAsync(externalId);
        return menu?.PreviousMenuItem;
    }
    
    private Task<MenuPanel?> GetAsync(long externalId)
    {
        return _databaseContext.Set<MenuPanel>()
            .FirstOrDefaultAsync(x => x.ExternalUser.PrivateId == externalId);
    }
}