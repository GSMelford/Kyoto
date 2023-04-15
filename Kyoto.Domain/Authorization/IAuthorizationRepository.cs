namespace Kyoto.Domain.Authorization;

public interface IAuthorizationRepository
{
    Task SaveUserAsync(User user);
}