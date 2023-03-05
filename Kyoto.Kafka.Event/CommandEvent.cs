using Kyoto.Domain.CommandPerformance;
using Kyoto.Domain.Telegram.Types;
using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class CommandEvent : BaseEvent
{
    public Command Command { get; set; } 
    public Message Message { get; set; } = null!;
}