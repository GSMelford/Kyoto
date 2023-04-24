using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.Authorization;
using Kyoto.Kafka.Event;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters;
using TBot.Client.Requests;
using User = Kyoto.Domain.Authorization.User;

namespace Kyoto.Bot.Services.Authorization;

public class AuthorizationService : IAuthorizationService
{
    private readonly ILogger<IAuthorizationService> _logger;
    private readonly IAuthorizationRepository _authorizationRepository;

    public AuthorizationService(ILogger<IAuthorizationService> logger, IAuthorizationRepository authorizationRepository)
    {
        _logger = logger;
        _authorizationRepository = authorizationRepository;
    }

    public Task RegisterAsync(User user)
    {
        return _authorizationRepository.SaveUserAsync(user);
    }
    
    
}