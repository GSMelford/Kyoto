using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Kafka.Handlers;

public class CallbackQueryHandler : IKafkaHandler<CallbackQueryEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public CallbackQueryHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task HandleAsync(CallbackQueryEvent callbackQueryEvent)
    {
        using (CurrentBotTenant.SetBotTenant(BotTenantModel.Create(callbackQueryEvent.TenantKey)))
        {
            using var scope = _serviceProvider.CreateScope();
            var callbackQueryService = scope.ServiceProvider.GetRequiredService<ICallbackQueryService>();
            await callbackQueryService.ProcessAsync(callbackQueryEvent.GetSession(), callbackQueryEvent.CallbackQuery);
        }
    }
}