using Kyoto.Domain.System;

namespace Kyoto.Kafka.Event;

public class RequestEvent : BaseSessionEvent
{
    public string Endpoint { get; set; } = null!;
    public HttpMethod HttpMethod { get; set; } = null!;
    public Dictionary<string, string> Parameters { get; set; } = new ();

    public RequestEvent()
    {
        
    }
    
    public RequestEvent(Session session) : base(session)
    {
        
    }
}