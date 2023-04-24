namespace Kyoto.Kafka.Event;

public class RequestEvent : BaseSessionEvent
{
    public string Endpoint { get; set; } = null!;
    public HttpMethod HttpMethod { get; set; } = null!;
    public Dictionary<string, string>? Headers = new ();
    public Dictionary<string, string> Parameters = new ();
}