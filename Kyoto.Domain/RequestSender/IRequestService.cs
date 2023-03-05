namespace Kyoto.Domain.RequestSender;

public interface IRequestService
{
    Task SendRequestAsync(Guid sessionId, Request request);
}