using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Kafka.Handlers.BotFactory;

public class CallbackQueryHandler : IKafkaHandler<CallbackQueryEvent>
{
    private readonly ICallbackQueryService _callbackQueryService;

    public CallbackQueryHandler(ICallbackQueryService callbackQueryService)
    {
        _callbackQueryService = callbackQueryService;
    }

    public Task HandleAsync(CallbackQueryEvent callbackQueryEvent)
    {
        return _callbackQueryService.ProcessAsync(callbackQueryEvent.GetSession(), callbackQueryEvent.CallbackQuery);
    }
}