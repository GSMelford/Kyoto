namespace Kyoto.Domain.PostSystem;

public class Request
{
    public string Endpoint { get; }
    public HttpMethod Method { get; }
    public Dictionary<string, string> Parameters { get; }

    public Request(
        string endpoint, 
        HttpMethod method, 
        Dictionary<string, string> parameters)
    {
        Endpoint = endpoint;
        Method = method;
        Parameters = parameters;
    }

    public static Request Create(string endpoint, HttpMethod method, Dictionary<string, string> parameters)
    {
        return new Request(endpoint, method, parameters);
    } 
}