using Kyoto.Domain.Command;
using Kyoto.Domain.Menu;
using Kyoto.Domain.System;

namespace Kyoto.Bot.Services.Menu;

public class MenuService : IMenuService
{
    private readonly MenuPanelPostService _menuPanelPostService;
    private readonly MenuPostService _menuPostService;
    private readonly IMenuRepository _menuRepository;
    private readonly IExecutiveCommandRepository _executiveCommandRepository;

    public MenuService(
        MenuPanelPostService menuPanelPostService, 
        MenuPostService menuPostService, 
        IMenuRepository menuRepository, 
        IExecutiveCommandRepository executiveCommandRepository)
    {
        _menuPanelPostService = menuPanelPostService;
        _menuPostService = menuPostService;
        _menuRepository = menuRepository;
        _executiveCommandRepository = executiveCommandRepository;
    }

    public async Task ExecuteIfCommandAsync(Session session, string value)
    {
        string? newMenuPanel = await DoAsync(session, value);

        if (value == MenuButtons.Back)
        {
            string? menuPanelName = await _menuRepository.GetPreviousMenuPanelAsync(session.ExternalUserId);
            if (!string.IsNullOrEmpty(menuPanelName))
            {
                newMenuPanel = await DoAsync(session, menuPanelName);
            }
        }

        if (!string.IsNullOrEmpty(newMenuPanel))
        {
            await _menuRepository.CreateOrUpdateAsync(session.ExternalUserId, newMenuPanel);
        }
    }

    private async Task<string?> DoAsync(Session session, string value)
    {
        switch (value)
        {
            case MenuButtons.BotManagement:
                await _menuPanelPostService.SendBotManagementAsync(session);
                return MenuButtons.BotManagement;
            case MenuButtons.BotManagementButtons.RegisterNewBot:
                await _executiveCommandRepository.SaveExecutiveCommandAsync(session, ExecutiveCommandType.RegisterBot);
                await _menuPostService.StartRegisterNewBotAsync(session);
                return null;
        }

        return null;
    }
}