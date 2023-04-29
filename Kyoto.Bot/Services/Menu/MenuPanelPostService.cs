using Kyoto.Bot.Services.PostSystem;
using Kyoto.Domain.Menu;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.Services.Menu;

public class MenuPanelPostService
{
    private readonly IPostService _postService;
    private readonly IMenuRepository _menuRepository;

    public MenuPanelPostService(IPostService postService, IMenuRepository menuRepository)
    {
        _postService = postService;
        _menuRepository = menuRepository;
    }
    
    public Task SendMenuAsync(Session session)
    {
        var keyboard = new ReplyKeyboardMarkup { OneTimeKeyboard = true, ResizeKeyboard = true}
            .Add(new KeyboardButton
            {
                Text = MenuButtons.BotManagement
            });
        
        return _postService.PostAsync(session, new SendMessageRequest(new SendMessageParameters
        {
            Text = "📃 Kyoto Bot Menu ⬇️",
            ChatId = session.ChatId,
            ReplyMarkup = keyboard
        }).ToRequest());
    }
    
    public Task SendBotManagementAsync(Session session)
    {
        var keyboard = new ReplyKeyboardMarkup { OneTimeKeyboard = true, ResizeKeyboard = true }
            .Add(new KeyboardButton
            {
                Text = MenuButtons.BotManagementButtons.RegisterNewBot
            })
            .AddNextLine()
            .Add(new KeyboardButton
            {
                Text = MenuButtons.BotManagementButtons.DeployBot
            })
            .Add(new KeyboardButton
            {
                Text = MenuButtons.BotManagementButtons.DisableBot
            })
            .AddNextLine()
            .Add(new KeyboardButton
            {
                Text = MenuButtons.BotManagementButtons.DeleteBot
            })
            .Add(new KeyboardButton
            {
                Text = MenuButtons.Back
            });
        
        return _postService.PostAsync(session, new SendMessageRequest(new SendMessageParameters
        {
            Text = MenuButtons.BotManagement,
            ChatId = session.ChatId,
            ReplyMarkup = keyboard
        }).ToRequest());
    }
}