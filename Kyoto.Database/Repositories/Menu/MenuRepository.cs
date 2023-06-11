using Kyoto.Database.CommonModels;
using Kyoto.Domain.Menu.Interfaces;
using Microsoft.EntityFrameworkCore;
using MenuButton = Kyoto.Database.CommonModels.MenuButton;
using MenuPanel = Kyoto.Database.CommonModels.MenuPanel;

namespace Kyoto.Database.Repositories.Menu;

public class MenuRepository : IMenuRepository
{
    private readonly IDatabaseContext _databaseContext;

    public MenuRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<bool> IsMenuPanelAsync(string name)
    {
        var menuPanelDal = await _databaseContext.Set<MenuPanel>()
            .FirstOrDefaultAsync(x => x.Name == name);

        return menuPanelDal is not null;
    }
    
    public async Task<Kyoto.Domain.Menu.MenuPanel> GetMenuPanelAsync(string menuPanelName)
    {
        var menuPanelDal = await _databaseContext.Set<MenuPanel>()
            .Include(x=>x.Buttons)
            .FirstAsync(x => x.Name == menuPanelName);

        return menuPanelDal.ToDomain();
    }
    
    public async Task<bool> IsMenuButtonAsync(string menuButtonText)
    {
        var menuButtonDal = await _databaseContext.Set<MenuButton>()
            .FirstOrDefaultAsync(x => x.Text == menuButtonText);

        return menuButtonDal is not null;
    }
    
    public async Task<string> GetMenuButtonCodeAsync(string menuButtonText)
    {
        var menuPanelDal = await _databaseContext.Set<MenuButton>()
            .FirstAsync(x => x.Text == menuButtonText);

        return menuPanelDal.Code;
    }
    
    public async Task SetMenuButtonStatusAsync(string menuButtonText, bool isEnable = true)
    {
        var menuPanelDal = await _databaseContext.Set<MenuButton>()
            .FirstAsync(x => x.Text == menuButtonText);

        menuPanelDal.IsEnable = isEnable;
        _databaseContext.Update(menuPanelDal);
        await _databaseContext.SaveChangesAsync();
    }
    
    public async Task AddAccessToWatchButtonAsync(long externalId, string menuButtonText)
    {
        var (accessToButton, menuButton, externalUser) = await GetMenuButtonInfoAsync(externalId, menuButtonText);

        if (accessToButton is null)
        {
            await _databaseContext.AddAsync(new MenuButtonAccess
            {
                MenuButton = menuButton,
                ExternalUser = externalUser
            });
            await _databaseContext.SaveChangesAsync();
        }
    }
    
    public async Task RemoveAccessToWatchButtonAsync(long externalId, string menuButtonText)
    {
        var (accessToButton, _, _) = await GetMenuButtonInfoAsync(externalId, menuButtonText);

        if (accessToButton is not null)
        { 
            _databaseContext.Remove(accessToButton);
            await _databaseContext.SaveChangesAsync();
        }
    }

    public async Task<bool> IsAccessToWatchExistAsync(long externalId, Guid menuButtonId)
    {
        var accessToButton = await _databaseContext.Set<MenuButtonAccess>()
            .Include(x=>x.ExternalUser)
            .FirstOrDefaultAsync(x => x.MenuButtonId == menuButtonId && x.ExternalUser.PrivateId == externalId);
        
        return accessToButton is not null;
    }
    
    private async Task<(MenuButtonAccess?, MenuButton, ExternalUser)> GetMenuButtonInfoAsync(long externalId, string menuButtonText)
    {
        var menuButton = await _databaseContext.Set<MenuButton>()
            .FirstAsync(x => x.Text == menuButtonText);

        var externalUser = await _databaseContext.Set<ExternalUser>()
            .FirstAsync(x => x.PrivateId == externalId);

        var accessToButton = await _databaseContext.Set<MenuButtonAccess>()
            .Include(x=>x.MenuButton)
            .Include(x=>x.ExternalUser)
            .FirstOrDefaultAsync(x => x.MenuButton.Id == menuButton.Id && x.ExternalUser.Id == externalUser.Id);

        return (accessToButton, menuButton, externalUser);
    }
}