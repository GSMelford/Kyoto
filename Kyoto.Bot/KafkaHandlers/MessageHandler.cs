using Kyoto.Domain.Processors;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers;

public class MessageHandler : IEventHandler<MessageEvent>
{
    private readonly IMessageService _messageService;

    public MessageHandler(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public Task HandleAsync(MessageEvent @event)
    {
        return _messageService.ProcessAsync(@event.SessionId, @event.Message);
    }
}