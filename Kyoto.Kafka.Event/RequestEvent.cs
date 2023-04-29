namespace Kyoto.Kafka.Event;

public class RequestEvent : BaseSessionEvent
{
    public string Endpoint { get; set; } = null!;
    public HttpMethod HttpMethod { get; set; } = null!;
    public Dictionary<string, string>? Headers { get; set; } = new ();
    public Dictionary<string, string> Parameters { get; set; } = new ();
    public ResponseMessageReturn? ReturnResponse { get; set; }
}

public class ResponseMessageReturn
{
    public string HandlerType { get; set; } = null!;
}