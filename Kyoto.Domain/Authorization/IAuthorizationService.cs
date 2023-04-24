namespace Kyoto.Domain.Authorization;

public interface IAuthorizationService
{
    Task RegisterAsync(User user);
}