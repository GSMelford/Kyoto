using Kyoto.Domain.Menu;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Services.BotFactory.PostSystem;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Services.Menu;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IPostService _postService;

    public MenuService(IMenuRepository menuRepository, IPostService postService)
    {
        _menuRepository = menuRepository;
        _postService = postService;
    }

    public async Task<(bool, string)> TryGetMenuCommandCodeAsync(string menuButtonText)
    {
        if (await _menuRepository.IsMenuButtonAsync(menuButtonText))
        {
            var code = await _menuRepository.GetMenuButtonCodeAsync(menuButtonText);
            return (true, code);
        }

        return (false, string.Empty);
    }
    
    public async Task SendHomeMenuAsync(Session session)
    {
        var menuPanel = await _menuRepository.GetMenuPanelAsync(MenuPanelConstants.HomeMenuPanel);
        await SendMenuAsync(session, menuPanel);
    }
    
    public async Task<bool> SendMenuIfExistsAsync(Session session, string menuButtonText)
    {
        var tempMenuPanelName = menuButtonText.Replace($"{MenuPanelConstants.Back}", "");
        if (!await _menuRepository.IsMenuPanelAsync(tempMenuPanelName))
            return false;

        var menuPanel = await _menuRepository.GetMenuPanelAsync(tempMenuPanelName);
        await SendMenuAsync(session, menuPanel);
        return true;
    }

    private async Task SendMenuAsync(Session session, MenuPanel menuPanel)
    {
        var maxLine = menuPanel.MenuButtons.Max(x=>x.Line) + 1;
        var keyboard = new ReplyKeyboardMarkup { OneTimeKeyboard = true, ResizeKeyboard = true };

        for (int i = 0; i < maxLine; i++)
        {
            bool isMenuButtonAdded = false;
            var maxIndex = menuPanel.MenuButtons.Where(x=>x.Line == i).Max(x=>x.Index) + 1;
            for (int j = 0; j < maxIndex; j++)
            {
                var i1 = i;
                var j1 = j;
                
                var menuButtons = menuPanel.MenuButtons.Where(x => x.Line == i1 && x.Index == j1);

                foreach (var menuButton in menuButtons)
                {
                    if (!menuButton.IsEnable)
                    {
                        continue;
                    }
                    
                    if (menuButton.IsNeedAccessToWatch)
                    {
                        if (!await _menuRepository.IsAccessToWatchExistAsync(session.ExternalUserId, menuButton.Id))
                            continue;
                    }

                    isMenuButtonAdded = true;
                    keyboard.Add(new KeyboardButton
                    {
                        Text = menuButton.Text
                    });
                }
            }

            if (i + 1 != maxLine && isMenuButtonAdded) 
                keyboard.AddNextLine();
        }

        await _postService.PostAsync(session, new SendMessageRequest(new SendMessageParameters
        {
            Text = menuPanel.Name,
            ReplyMarkup = keyboard,
            ChatId = session.ChatId
        }).ToRequest());
    }
}