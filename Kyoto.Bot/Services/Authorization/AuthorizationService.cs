using Kyoto.Domain.Authorization;
using Kyoto.Domain.Telegram.Types;
using User = Kyoto.Domain.Authorization.User;

namespace Kyoto.Bot.Services.Authorization;

public class AuthorizationService : IAuthorizationService
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public AuthorizationService(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public Task RegisterAsync(string username, Contact contact)
    {
        var user = User.Create(contact.FirstName, contact.LastName, contact.UserId, username, contact.PhoneNumber);
        return _authorizationRepository.SaveUserAsync(user);
    }
}