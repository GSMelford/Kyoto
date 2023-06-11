using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using Microsoft.Extensions.DependencyInjection;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Kafka.Handlers;

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
            var postService = scope.ServiceProvider.GetRequiredService<IPostService>();

            var session = commandEvent.GetSession();
            if(CommandCodes.Cancel == commandEvent.Name)
            {
                var canceledCommand = await commandService.CancelCommandAsync(session);
                await postService.PostAsync(session, new SendMessageRequest(new SendMessageParameters
                {
                    Text = $"😶‍🌫️ Command {canceledCommand} was interrupted",
                    ChatId = session.ChatId
                }).ToRequest());
            }
            
            await commandService.ProcessCommandAsync(session , commandEvent.Name, message: commandEvent.Message);
        }
    }
}