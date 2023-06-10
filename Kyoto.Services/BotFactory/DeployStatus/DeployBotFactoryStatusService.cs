using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Settings;
using TBot.Client.Parameters;
using TBot.Client.Parameters.Webhook;
using TBot.Client.Requests;
using TBot.Client.Requests.Webhook;

namespace Kyoto.Services.BotFactory.DeployStatus;

public class DeployBotFactoryStatusService : IDeployStatusService
{
    private readonly IPostService _postService;
    private readonly IBotRepository _botRepository;
    private readonly BotTenantSettings _botTenantSettings;
    private readonly KyotoBotFactorySettings _kyotoBotFactorySettings;
    
    public DeployBotFactoryStatusService(
        IPostService postService, 
        IBotRepository botRepository,
        BotTenantSettings botTenantSettings,
        KyotoBotFactorySettings kyotoBotFactorySettings)
    {
        _postService = postService;
        _botRepository = botRepository;
        _botTenantSettings = botTenantSettings;
        _kyotoBotFactorySettings = kyotoBotFactorySettings;
    }

    public async Task Notify(Session session)
    {
        string newTenant = session.TenantKey;
        await _botRepository.SetBotStatusesAsync(session.ChatId, session.TenantKey, true, true);
        var newSession = Session.CreatePersonalNew(_botTenantSettings.Key, session.ChatId);
        
        await _postService.PostAsync(session, new SetWebhookRequest(new SetWebhookParameters
        {
            Url = $"{_kyotoBotFactorySettings.BaseUrl}{_kyotoBotFactorySettings.ReceiverEndpoint}",
            SecretToken = newTenant
        }).ToRequest());
        
        await _postService.PostAsync(newSession, new SendMessageRequest(new SendMessageParameters
        {
            Text = $"Бот {newTenant} успішно запущений! 🤖🚀",
            ChatId = newSession.ChatId
        }).ToRequest());
    }
}