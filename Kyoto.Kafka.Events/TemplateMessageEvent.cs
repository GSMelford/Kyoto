using Kyoto.Domain.System;
using Kyoto.Domain.TemplateMessage;

namespace Kyoto.Kafka.Event;

public class TemplateMessageEvent : BaseSessionEvent
{
    public TemplateMessageEventAction Action { get; set; }
    public TemplateMessageTypeValue Type { get; set; }
    public string? Text { get; set; }
    
    public TemplateMessageEvent()
    {
        
    }
    
    public TemplateMessageEvent(Session session) : base(session)
    {
        
    }
}

public enum TemplateMessageEventAction
{
    Send,
    Update
}