using Kyoto.Domain.BotFactory.Menu;
using Kyoto.Domain.BotFactory.Menu.Interfaces;
using Kyoto.Domain.System;

namespace Kyoto.Services.BotFactory.Menu;

public class MenuService : IMenuService
{
    private readonly MenuPanelPostService _menuPanelPostService;
    private readonly IMenuRepository _menuRepository;

    public MenuService(MenuPanelPostService menuPanelPostService, IMenuRepository menuRepository)
    {
        _menuPanelPostService = menuPanelPostService;
        _menuRepository = menuRepository;
    }

    public async Task DrawMenuIfExist(Session session, string value)
    {
        if (value == MenuButtons.Back)
        {
            string? menuPanelName = await _menuRepository.GetPreviousMenuPanelAsync(session.ExternalUserId);
            if (!string.IsNullOrEmpty(menuPanelName)) {
                await DrawMenuAsync(session, menuPanelName);
            }
        }
        
        string? newMenuPanel = await DrawMenuAsync(session, value);
        if (!string.IsNullOrEmpty(newMenuPanel)) {
            await _menuRepository.CreateOrUpdateAsync(session.ExternalUserId, newMenuPanel);
        }
    }

    private async Task<string?> DrawMenuAsync(Session session, string value)
    {
        switch (value)
        {
            case MenuButtons.BotManagement:
                await _menuPanelPostService.SendBotManagementAsync(session);
                return MenuButtons.BotManagement;
        }

        return null;
    }
}