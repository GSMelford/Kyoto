using Kyoto.Domain.System;
using Kyoto.Domain.Tenant;
using Kyoto.Telegram.Sender.Domain;
using Kyoto.Telegram.Sender.Interfaces;
using TBot.Client.Interfaces;
using TBot.Core.RequestArchitecture;
using TBot.Core.RequestArchitecture.Structure;

namespace Kyoto.Telegram.Sender.Services;

public class RequestService : IRequestService
{
    private readonly IServiceProvider _serviceProvider;

    public RequestService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<HttpResponseMessage> SendAsync(Session session, RequestModel requestModel)
    {
        var botTenantModel = BotTenantFactory.Store.Get(session.TenantKey);
        using (CurrentBotTenant.SetBotTenant(botTenantModel))
        {
            var bot = _serviceProvider.GetRequiredService<ITBot>();
            requestModel.Parameters.TryGetValue("chat_id", out var key);

            return await bot.PostAsync(new BaseRequest(
                requestModel.Endpoint,
                requestModel.HttpMethod,
                requestModel.Parameters.Select(x => new Parameter(x.Key, x.Value)).ToList()), key);
        }
    }
}