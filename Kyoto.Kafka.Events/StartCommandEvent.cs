using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Kafka.Event;

public class StartCommandEvent : BaseSessionEvent
{
    public string UserFirstName { get; set; } = null!;
    
    public StartCommandEvent()
    {
        
    }
    
    public StartCommandEvent(Session session) : base(session)
    {
        
    }
}