using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.Authorization;
using Kyoto.Domain.RequestSender;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Bot.KafkaHandlers.CommandHandlers;

public class RegisterHandler : IEventHandler<RegisterEvent>
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IRequestService _requestService;

    public RegisterHandler(IAuthorizationService authorizationService, IRequestService requestService)
    {
        _authorizationService = authorizationService;
        _requestService = requestService;
    }

    public async Task HandleAsync(RegisterEvent @event)
    {
        await _authorizationService.RegisterAsync(@event.Username, @event.Contact);
        await _requestService.SendRequestAsync(@event.SessionId, new SendMessageRequest(new SendMessageParameters
        {
            Text = $"Thank you for registering, {@event.Contact.FirstName}! 💞",
            ChatId = @event.ChatId
        }).ToDomain());
    }
}