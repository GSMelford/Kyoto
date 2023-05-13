using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Settings;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Services.BotFactory.DeployStatus;

public class DeployBotFactoryStatusService : IDeployStatusService
{
    private readonly IPostService _postService;
    private readonly IBotRepository _botRepository;
    private readonly BotTenantSettings _botTenantSettings;
    
    public DeployBotFactoryStatusService(IPostService postService, IBotRepository botRepository, BotTenantSettings botTenantSettings)
    {
        _postService = postService;
        _botRepository = botRepository;
        _botTenantSettings = botTenantSettings;
    }

    public async Task Notify(Session session)
    {
        string newTenant = session.TenantKey;
        await _botRepository.SetActiveBotAsync(session.ChatId, session.TenantKey);
        var newSession = Session.CreatePersonalNew(_botTenantSettings.Key, session.ChatId);
        
        await _postService.PostAsync(newSession, new SendMessageRequest(new SendMessageParameters
        {
            Text = $"Bot {newTenant} deployed successfully! 🤖🚀",
            ChatId = newSession.ChatId
        }).ToRequest());
    }
}