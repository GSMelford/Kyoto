using Kyoto.Domain.CommandServices;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers.CommandHandlers;

public class StartCommandHandler : IEventHandler<StartCommandEvent>
{
    private readonly IStartCommandService _startCommandService;

    public StartCommandHandler(IStartCommandService startCommandService)
    {
        _startCommandService = startCommandService;
    }

    public async Task HandleAsync(StartCommandEvent startCommandEvent)
    {
        await _startCommandService.ExecuteAsync(
            startCommandEvent.SessionId, 
            startCommandEvent.ChatId,
            startCommandEvent.TelegramId);
    }
}