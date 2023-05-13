using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Kafka.Handlers.BotFactory;

public class CommandHandler : IKafkaHandler<CommandEvent>
{
    private readonly ICommandService _commandService;

    public CommandHandler(ICommandService commandService)
    {
        _commandService = commandService;
    }

    public Task HandleAsync(CommandEvent commandEvent)
    {
        return _commandService.ProcessCommandAsync(commandEvent.GetSession(), commandEvent.Name, message: commandEvent.Message);
    }
}