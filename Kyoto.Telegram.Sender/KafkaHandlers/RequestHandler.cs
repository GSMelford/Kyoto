using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using TBot.Client.Interfaces;
using TBot.Core.RequestArchitecture;
using TBot.Core.RequestArchitecture.Structure;

namespace Kyoto.Telegram.Sender.KafkaHandlers;

public class RequestHandler : IEventHandler<RequestEvent>
{
    private readonly ILogger<IEventHandler<RequestEvent>> _logger;
    private readonly ITBot _tBot;

    public RequestHandler(ILogger<IEventHandler<RequestEvent>> logger, ITBot tBot)
    {
        _logger = logger;
        _tBot = tBot;
    }

    public async Task HandleAsync(RequestEvent requestEvent)
    {
        requestEvent.Parameters.TryGetValue("chat_id", out string? key);

        await _tBot.PostAsync(new BaseRequest(
            requestEvent.Endpoint,
            requestEvent.HttpMethod,
            requestEvent.Parameters.Select(x => new Parameter(x.Key, x.Value)).ToList()), key);
        
        _logger.LogInformation("The message was successfully delivered to telegram. Session ended. SessionId: {SessionId}", requestEvent.SessionId);
    }
}