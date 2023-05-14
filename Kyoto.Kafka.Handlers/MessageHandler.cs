using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Kafka.Handlers;

public class MessageHandler : IKafkaHandler<MessageEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public MessageHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task HandleAsync(MessageEvent messageEvent)
    {
        using (CurrentBotTenant.SetBotTenant(BotTenantModel.Create(messageEvent.TenantKey)))
        {
            using var scope = _serviceProvider.CreateScope();
            var messageService = scope.ServiceProvider.GetRequiredService<IMessageService>(); 
            await messageService.ProcessAsync(messageEvent.GetSession(), messageEvent.Message);
        }
    }
}