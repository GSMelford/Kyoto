using Kyoto.Kafka.Modules;

namespace Kyoto.Kafka.Event;

public class RequestEvent : BaseEvent
{
    public string Endpoint { get; set; } = null!;
    public HttpMethod HttpMethod { get; set; } = null!;
    public Dictionary<string, string>? Headers = new ();
    public Dictionary<string, string> Parameters = new ();
}