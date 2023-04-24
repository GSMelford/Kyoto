using Kyoto.Domain.Command;
using Kyoto.Domain.Telegram.Types;
using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class CommandEvent : BaseSessionEvent
{
    public CommandType CommandType { get; set; } 
    public Message Message { get; set; } = null!;
}