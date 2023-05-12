using Kyoto.Domain.BotFactory.GlobalCommand;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Kafka.Event;

public class CommandEvent : BaseSessionEvent
{
    public GlobalCommandType CommandType { get; set; } 
    public Message Message { get; set; } = null!;
    
    public CommandEvent()
    {
        
    }
    
    public CommandEvent(Session session) : base(session)
    {
        
    }
}