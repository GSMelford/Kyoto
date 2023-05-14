using Kyoto.Domain.TemplateMessage;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Kafka.Handlers.BotClient;

public class TemplateMessageHandler : IKafkaHandler<TemplateMessageEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public TemplateMessageHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task HandleAsync(TemplateMessageEvent templateMessageEvent)
    {
        using (CurrentBotTenant.SetBotTenant(BotTenantModel.Create(templateMessageEvent.TenantKey)))
        {
            using var scope = _serviceProvider.CreateScope();
            var templateMessageService = scope.ServiceProvider.GetRequiredService<ITemplateMessageService>();

            if (templateMessageEvent.Action == TemplateMessageEventAction.Send)
            {
                await templateMessageService.SendTemplateMessageAsync(
                    templateMessageEvent.GetSession(),
                    templateMessageEvent.Type);
            }
        }
    }
}