using Kyoto.Domain.Menu.Interfaces;
using Microsoft.EntityFrameworkCore;
using MenuButton = Kyoto.Database.CommonModels.MenuButton;
using MenuPanel = Kyoto.Database.CommonModels.MenuPanel;

namespace Kyoto.Database.CommonRepositories.Menu;

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
    
    public async Task EnableMenuAsync(string menuButtonText)
    {
        var menuPanelDal = await _databaseContext.Set<MenuButton>()
            .FirstAsync(x => x.Text == menuButtonText);

        menuPanelDal.IsEnable = true;
        await _databaseContext.SaveAsync(menuPanelDal);
    }
}