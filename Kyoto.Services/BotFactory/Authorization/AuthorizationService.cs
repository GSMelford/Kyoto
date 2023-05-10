using Kyoto.Domain.BotFactory.Authorization.Interfaces;
using Microsoft.Extensions.Logging;
using User = Kyoto.Domain.BotFactory.Authorization.User;

namespace Kyoto.Services.BotFactory.Authorization;

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