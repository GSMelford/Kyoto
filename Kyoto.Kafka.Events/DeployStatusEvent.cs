using Kyoto.Domain.System;

namespace Kyoto.Kafka.Event;

public class DeployStatusEvent : BaseSessionEvent
{
    public DeployStatusEvent()
    {
        
    }
    
    public DeployStatusEvent(Session session) : base(session)
    {
        
    }
}