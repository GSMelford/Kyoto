using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Kafka.Event;

public class CommandEvent : BaseSessionEvent
{
    public string Name { get; set; } = null!;
    public Message Message { get; set; } = null!;
    
    public CommandEvent()
    {
        
    }
    
    public CommandEvent(Session session) : base(session)
    {
        
    }
}