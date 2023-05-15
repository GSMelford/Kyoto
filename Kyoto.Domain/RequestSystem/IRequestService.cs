namespace Kyoto.Domain.RequestSystem;

public interface IRequestService
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage);
    Task<bool> SendWithStatusCodeAsync(HttpRequestMessage httpRequestMessage);
    Task<T> SendAsync<T>(HttpRequestMessage httpRequestMessage);
}