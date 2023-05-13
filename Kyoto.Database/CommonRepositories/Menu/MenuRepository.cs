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

    public async Task<bool> IsMenuPanelButtonAsync(string name)
    {
        var menuButtonDal = await _databaseContext.Set<MenuButton>()
            .Include(x=>x.MenuPanel)
            .FirstOrDefaultAsync(x => x.Text == name && x.MenuPanel != null);

        return menuButtonDal is not null;
    }
    
    public async Task<Kyoto.Domain.Menu.MenuPanel> GetMenuPanelAsync(string menuPanelName)
    {
        var menuPanelDal = await _databaseContext.Set<MenuPanel>()
            .Include(x=>x.Buttons)
            .FirstAsync(x => x.Name == menuPanelName);

        return menuPanelDal.ToDomain();
    }
}