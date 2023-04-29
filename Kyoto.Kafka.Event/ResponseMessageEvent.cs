using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Kafka.Event;

public class ResponseMessageEvent : BaseSessionEvent
{
    public Message Message { get; set; } = null!;
    public ResponseMessageReturn ResponseMessageReturn { get; set; } = null!;
}