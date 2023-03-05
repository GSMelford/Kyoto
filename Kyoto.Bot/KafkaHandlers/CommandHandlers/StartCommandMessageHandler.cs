using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.RequestSender;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Bot.KafkaHandlers.CommandHandlers;

public class StartCommandMessageHandler : IEventHandler<StartCommandEvent>
{
    private readonly ILogger<IEventHandler<StartCommandEvent>> _logger;
    private readonly IRequestService _requestService;

    public StartCommandMessageHandler(ILogger<IEventHandler<StartCommandEvent>> logger, IRequestService requestService)
    {
        _logger = logger;
        _requestService = requestService;
    }

    public async Task HandleAsync(StartCommandEvent startCommandEvent)
    {
        var request = new SendMessageRequest(new SendMessageParameters
        {
            Text = "Welcome!",
            ChatId = startCommandEvent.ChatId
        }).ToDomain();
        
        await _requestService.SendRequestAsync(startCommandEvent.SessionId, request);
        _logger.LogInformation("The command Start has been processed and sent to telegram. SessionId: {SessionId}", startCommandEvent.SessionId);
    }
}