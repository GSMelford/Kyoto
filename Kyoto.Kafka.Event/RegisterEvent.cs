using Kyoto.Domain.Telegram.Types;
using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class RegisterEvent : BaseEvent
{
    public Contact Contact { get; set; } = null!;
    public long ChatId { get; set; }
    public string Username { get; set; } = null!;
}