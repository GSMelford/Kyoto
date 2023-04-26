using Kyoto.Domain.Command.GlobalCommand;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Kafka.Event;

public class CommandEvent : BaseSessionEvent
{
    public GlobalCommandType GlobalCommandType { get; set; } 
    public Message Message { get; set; } = null!;
}