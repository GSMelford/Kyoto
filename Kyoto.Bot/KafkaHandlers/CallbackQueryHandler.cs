using Kyoto.Domain.Processors;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers;

public class CallbackQueryHandler : IEventHandler<CallbackQueryEvent>
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