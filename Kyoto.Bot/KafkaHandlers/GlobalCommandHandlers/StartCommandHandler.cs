using Kyoto.Domain.Command.GlobalCommand;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers.GlobalCommandHandlers;

public class StartCommandHandler : IKafkaHandler<StartCommandEvent>
{
    private readonly IStartCommandService _startCommandService;

    public StartCommandHandler(IStartCommandService startCommandService)
    {
        _startCommandService = startCommandService;
    }

    public async Task HandleAsync(StartCommandEvent startCommandEvent)
    {
        await _startCommandService.ExecuteAsync(startCommandEvent.GetSession());
    }
}