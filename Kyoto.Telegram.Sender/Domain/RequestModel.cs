namespace Kyoto.Telegram.Sender.Domain;

public class RequestModel
{
    public string Endpoint { get; private set; }
    public HttpMethod HttpMethod { get; private set; }
    public Dictionary<string, string> Parameters { get; private set; }

    private RequestModel(
        string endpoint, 
        HttpMethod httpMethod,
        Dictionary<string, string> parameters)
    {
        Endpoint = endpoint;
        HttpMethod = httpMethod;
        Parameters = parameters;
    }

    public static RequestModel Create(
        string endpoint, 
        HttpMethod httpMethod, 
        Dictionary<string, string> parameters)
    {
        return new RequestModel(endpoint, httpMethod, parameters);
    }
}