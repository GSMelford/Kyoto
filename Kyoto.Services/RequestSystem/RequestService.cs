using Kyoto.Domain.RequestSystem;
using Newtonsoft.Json;

namespace Kyoto.Services.RequestSystem;

public class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;

    public RequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage)
    {
        return _httpClient.SendAsync(httpRequestMessage);
    }
    
    public async Task<bool> SendWithStatusCodeAsync(HttpRequestMessage httpRequestMessage)
    {
        var response = await _httpClient.SendAsync(httpRequestMessage);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<T> SendAsync<T>(HttpRequestMessage httpRequestMessage)
    {
        var response = await _httpClient.SendAsync(httpRequestMessage);
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(content)!;
    }
}