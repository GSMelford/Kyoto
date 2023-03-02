using Kyoto.Domain.Telegram.Types;
using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class MessageEvent : BaseEvent
{
    public Message Message { get; set; } = null!;
}