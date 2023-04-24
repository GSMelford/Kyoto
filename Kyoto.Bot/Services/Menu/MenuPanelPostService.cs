using Kyoto.Bot.Services.RequestSender;
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

    public MenuPanelPostService(IPostService postService)
    {
        _postService = postService;
    }
    
    public Task SendMenuAsync(Session session)
    {
        var keyboard = new ReplyKeyboardMarkup { OneTimeKeyboard = true }
            .Add(new KeyboardButton
            {
                Text = MenuButtons.BotManagement
            });
        
        return _postService.SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = "📃 Kyoto Bot Menu ⬇️",
            ChatId = session.ChatId,
            ReplyMarkup = keyboard
        }).ToDomain());
    }
    
    public Task SendBotManagementAsync(Session session)
    {
        var keyboard = new ReplyKeyboardMarkup { OneTimeKeyboard = true }
            .Add(new KeyboardButton
            {
                Text = MenuButtons.BotManagementButtons.RegisterNewBot
            })
            .Add(new KeyboardButton
            {
                Text = MenuButtons.BotManagementButtons.DeleteBot
            })
            .Add(new KeyboardButton
            {
                Text = MenuButtons.Back
            });
        
        return _postService.SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = MenuButtons.BotManagement,
            ChatId = session.ChatId,
            ReplyMarkup = keyboard
        }).ToDomain());
    }
}