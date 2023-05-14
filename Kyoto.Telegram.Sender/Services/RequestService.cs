using Kyoto.Domain.System;
using Kyoto.Domain.Tenant;
using Kyoto.Settings;
using Kyoto.Telegram.Sender.Domain;
using Kyoto.Telegram.Sender.Interfaces;
using TBot.Client;
using TBot.Client.Interfaces;
using TBot.Core.RequestArchitecture;
using TBot.Core.RequestArchitecture.Structure;

namespace Kyoto.Telegram.Sender.Services;

public class RequestService : IRequestService
{
    private readonly ITBot _tBot;
    private readonly KyotoBotSenderSettings _kyotoBotSenderSettings;

    public RequestService(ITBot tBot, KyotoBotSenderSettings kyotoBotSenderSettings)
    {
        _tBot = tBot;
        _kyotoBotSenderSettings = kyotoBotSenderSettings;
    }

    public async Task<HttpResponseMessage> SendAsync(Session session, RequestModel requestModel)
    {
        var botTenantModel = BotTenantFactory.Store.Get(session.TenantKey);
        requestModel.Parameters.TryGetValue("chat_id", out var chatId);

        _tBot.Init(new TBotOptions(botTenantModel.Token));

        var request = new BaseRequest(
            requestModel.Endpoint,
            requestModel.HttpMethod,
            requestModel.Parameters.Select(x => new Parameter(x.Key, x.Value)).ToList());
        
        if (_kyotoBotSenderSettings.IsLimiterEnable)
        {
            return await _tBot.PostWithLimiterAsync(request, $"{botTenantModel.TenantKey}:{chatId!}");
        }
        
        return await _tBot.PostAsync(request);
    }
}