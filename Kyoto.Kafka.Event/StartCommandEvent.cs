using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class StartCommandEvent : BaseEvent
{
    public int ChatId { get; set; }
    public long TelegramUserId { get; set; }
}