namespace Kyoto.Domain.PostSystem;

public class Request
{
    public string Endpoint { get; }
    public HttpMethod Method { get; }
    public Dictionary<string, string>? Headers { get; }
    public Dictionary<string, string> Parameters { get; }

    public Request(
        string endpoint, 
        HttpMethod method, 
        Dictionary<string, string>? headers,
        Dictionary<string, string> parameters)
    {
        Endpoint = endpoint;
        Method = method;
        Headers = headers;
        Parameters = parameters;
    }
}