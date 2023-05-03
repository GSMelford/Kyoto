using Kyoto.Bot.Services.PostSystem;
using Kyoto.Bot.StartUp.Settings;
using Kyoto.Domain.Bot;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using TBot.Client.Parameters.Webhook;
using TBot.Client.Requests.Webhook;

namespace Kyoto.Bot.Services.Bot;

public class BotService : IBotService
{
    private readonly AppSettings _appSettings;
    private readonly IBotRepository _botRepository;
    private readonly IPostService _postService;
    private readonly IKafkaProducer<string> _kafkaProducer;
    private readonly ITenantRepository _tenantRepository;

    public BotService(
        AppSettings appSettings,
        IBotRepository botRepository,
        IPostService postService, 
        IKafkaProducer<string> kafkaProducer, 
        ITenantRepository tenantRepository)
    {
        _appSettings = appSettings;
        _botRepository = botRepository;
        _postService = postService;
        _kafkaProducer = kafkaProducer;
        _tenantRepository = tenantRepository;
    }

    public Task<Guid> SaveAsync(Session session, BotModel botModel)
    {
        return _botRepository.SaveAsync(session.ExternalUserId, botModel);
    }

    public async Task ActivateBotAsync(Session session, string username)
    {
        var botTenant = await _tenantRepository.GetBotTenantAsync(session.ExternalUserId, username);
        var newSession = Session.CreateNew(botTenant.TenantKey);
        
        await _kafkaProducer.ProduceAsync(new InitTenantEvent
        {
            SessionId = newSession.Id,
            Token = botTenant.TenantKey,
            TenantKey = botTenant.Token
        }, session.TenantKey);
        
        await _postService.PostAsync(newSession, new SetWebhookRequest(new SetWebhookParameters
        {
            Url = $"{_appSettings.BaseUrl}{_appSettings.ReceiverEndpoint}",
            SecretToken = botTenant.TenantKey
        }).ToRequest());

        await _botRepository.SetActiveBotAsync(session.ExternalUserId, username);
    }
}