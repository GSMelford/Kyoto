using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class StartCommandEvent : BaseEvent
{
    public long ChatId { get; set; }
    public long TelegramId { get; set; }
}