using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.Menu;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Bot.Services.Menu;

public class MenuPostService
{
    private readonly IPostService _postService;

    public MenuPostService(IPostService postService)
    {
        _postService = postService;
    }

    public async Task StartRegisterNewBotAsync(Session session)
    {
        await _postService.SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = "🪄 Send the bot token with the following message",
            ChatId = session.ChatId
        }).ToDomain());
    }
}