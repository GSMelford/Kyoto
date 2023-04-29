using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Telegram.Sender.Interfaces;

namespace Kyoto.Telegram.Sender.KafkaHandlers;

public class RequestHandler : IKafkaHandler<RequestEvent>
{
    private readonly IRequestService _requestService;

    public RequestHandler(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public Task HandleAsync(RequestEvent requestEvent)
    {
        return _requestService.SendAsync(requestEvent.GetSession(), requestEvent.ToDomain());
    }
}