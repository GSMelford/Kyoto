using Kyoto.Domain.System;
using Kyoto.Domain.Tenant;
using Kyoto.Telegram.Sender.Domain;
using Kyoto.Telegram.Sender.Interfaces;
using TBot.Client;
using TBot.Client.Interfaces;
using TBot.Core.RequestArchitecture;
using TBot.Core.RequestArchitecture.Structure;
using TBot.Core.RequestLimiter;

namespace Kyoto.Telegram.Sender.Services;

public class RequestService : IRequestService
{
    private readonly ITBot _tBot;

    public RequestService(ITBot tBot)
    {
        _tBot = tBot;
    }

    public async Task<HttpResponseMessage> SendAsync(Session session, RequestModel requestModel)
    {
        var botTenantModel = BotTenantFactory.Store.Get(session.TenantKey);
        requestModel.Parameters.TryGetValue("chat_id", out var key);

        ((BotClient)_tBot).Init(new BotSettings(botTenantModel.Token), new LimiterConfig{StoreName = botTenantModel.TenantKey});
        
        return await _tBot.PostWithLimiterAsync(new BaseRequest(
            requestModel.Endpoint,
            requestModel.HttpMethod,
            requestModel.Parameters.Select(x => new Parameter(x.Key, x.Value)).ToList()), key!);
    }
}