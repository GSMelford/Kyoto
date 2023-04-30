using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Kafka.Event;

public class ResponseMessageEvent : BaseSessionEvent
{
    public Message Message { get; set; } = null!;
    public ResponseMessageReturn ResponseMessageReturn { get; set; } = null!;
    
    public ResponseMessageEvent()
    {
        
    }
    
    public ResponseMessageEvent(Session session) : base(session)
    {
        
    }
}