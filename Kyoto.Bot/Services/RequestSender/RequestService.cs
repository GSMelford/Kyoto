using Kyoto.Domain.RequestSender;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.Services.RequestSender;

public class RequestService : IRequestService
{
    private readonly IKafkaProducer<string> _kafkaProducer;

    public RequestService(IKafkaProducer<string> kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }

    public async Task SendRequestAsync(Guid sessionId, Request request)
    {
        await _kafkaProducer.ProduceAsync(new RequestEvent
        { 
            SessionId = sessionId,
            Endpoint = request.Endpoint,
            HttpMethod = request.Method,
            Headers = request.Headers,
            Parameters = request.Parameters
        });
    }
}