using Kyoto.Domain.PostSystem;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.Services.RequestSender;

public class PostService : IPostService
{
    private readonly IKafkaProducer<string> _kafkaProducer;

    public PostService(IKafkaProducer<string> kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }

    public async Task SendAsync(Guid sessionId, Request request)
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