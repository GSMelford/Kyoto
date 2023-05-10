using Kyoto.Domain.BotFactory.GlobalCommand.Interfaces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Kafka.Handlers.BotFactory.GlobalCommandHandlers;

public class StartCommandHandler : IKafkaHandler<StartCommandEvent>
{
    private readonly IStartCommandService _startCommandService;

    public StartCommandHandler(IStartCommandService startCommandService)
    {
        _startCommandService = startCommandService;
    }

    public async Task HandleAsync(StartCommandEvent startCommandEvent)
    {
        await _startCommandService.ExecuteAsync(startCommandEvent.GetSession(), startCommandEvent.UserFirstName);
    }
}