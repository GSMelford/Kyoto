namespace Kyoto.Domain.PostSystem;

public interface IPostService
{
    Task SendAsync(Guid sessionId, Request request);
}