using System.Net.Http.Headers;

namespace Kyoto.Extensions;

public class RequestCreator
{
    private readonly HttpRequestMessage _httpRequestMessage;
    private readonly Dictionary<string, string?> _parameters = new();
    private readonly Dictionary<string, string?> _headers = new();

    private const string TENANT_KEY = "Tenant";

    public RequestCreator(HttpMethod method, string baseUri)
    {
        _httpRequestMessage = new HttpRequestMessage(method, new Uri(baseUri, UriKind.RelativeOrAbsolute));
    }
    
    public RequestCreator SetBody(string body)
    {
        _httpRequestMessage.Content = new StringContent(body);
        _httpRequestMessage.Content!.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return this;
    }

    public RequestCreator AddTenantHeader(string tenantKey)
    {
        _headers.Add(TENANT_KEY, tenantKey);
        return this;
    }
    
    public RequestCreator AddHeader(string key, string value)
    {
        _headers.Add(key, value);
        return this;
    }

    public RequestCreator AddParameters(Dictionary<string, string> parameters)
    {
        foreach (var parameter in parameters)
        {
            _parameters.Add(parameter.Key, parameter.Value);
        }
        
        return this;
    }

    public HttpRequestMessage Create() 
    {
        if (_parameters.Any())
        {
            UriBuilder uriBuilder = new(_httpRequestMessage.RequestUri!)
            {
                Query = string.Join("&", _parameters.Select(parameter => $"{parameter.Key}={Uri.EscapeDataString(parameter.Value?.ToString()!)}"))
            };
            
            _httpRequestMessage.RequestUri = uriBuilder.Uri;
        }

        if (_headers.Any())
        {
            foreach (var (key, value) in _headers)
            {
                _httpRequestMessage.Headers.TryAddWithoutValidation(key, value);
            }
        }

        return _httpRequestMessage;
    }
}