using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Services.BotClient.Deploy;

public class DeployStatusService : IDeployStatusService
{
    private readonly IPostService _postService;

    public DeployStatusService(IPostService postService)
    {
        _postService = postService;
    }

    public async Task Notify(Session session)
    {
        var newSession = Session.CreatePersonalNew(session.TenantKey, session.ExternalUserId);
        await _postService.PostAsync(newSession, new SendMessageRequest(new SendMessageParameters
        {
            Text = "Hello!👋\nYou activated me, now I can work with your clients 👨‍💻",
            ChatId = session.ChatId
        }).ToRequest());
        
        await _postService.PostAsync(newSession, new SendMessageRequest(new SendMessageParameters
        {
            Text = "You can customize my functionality in the bot factory 💅",
            ChatId = session.ChatId
        }).ToRequest());
    }
}