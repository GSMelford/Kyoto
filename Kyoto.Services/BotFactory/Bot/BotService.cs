using Kyoto.Domain.BotFactory.Bot;
using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Settings;
using TBot.Client.Parameters;
using TBot.Client.Parameters.Webhook;
using TBot.Client.Requests;
using TBot.Client.Requests.Webhook;

namespace Kyoto.Services.BotFactory.Bot;

public class BotService : IBotService
{
    private readonly KyotoBotFactorySettings _kyotoBotFactorySettings;
    private readonly IBotRepository _botRepository;
    private readonly IPostService _postService;
    private readonly IKafkaProducer<string> _kafkaProducer;
    private readonly ITenantRepository _tenantRepository;

    public BotService(
        KyotoBotFactorySettings kyotoBotFactorySettings,
        IBotRepository botRepository,
        IPostService postService, 
        IKafkaProducer<string> kafkaProducer, 
        ITenantRepository tenantRepository)
    {
        _kyotoBotFactorySettings = kyotoBotFactorySettings;
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
        var newSession = Session.CreatePersonalNew(botTenant.TenantKey, session.ChatId);
        
        await _kafkaProducer.ProduceAsync(new InitTenantEvent
        {
            SessionId = newSession.Id,
            Token = botTenant.Token,
            TenantKey = botTenant.TenantKey,
            OwnerExternalUserId = session.ChatId
        }, string.Empty);
    }
}