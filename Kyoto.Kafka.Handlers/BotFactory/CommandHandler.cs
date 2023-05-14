using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Kafka.Handlers.BotFactory;

public class CommandHandler : IKafkaHandler<CommandEvent>
{
    private readonly IServiceProvider _serviceProvider;
    
    public CommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task HandleAsync(CommandEvent commandEvent)
    {
        using (CurrentBotTenant.SetBotTenant(BotTenantModel.Create(commandEvent.TenantKey)))
        {
            using var scope = _serviceProvider.CreateScope();
            var commandService = scope.ServiceProvider.GetRequiredService<ICommandService>();
            await commandService.ProcessCommandAsync(
                commandEvent.GetSession(), commandEvent.Name, message: commandEvent.Message);
        }
    }
}