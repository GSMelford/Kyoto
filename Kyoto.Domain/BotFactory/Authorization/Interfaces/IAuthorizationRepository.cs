namespace Kyoto.Domain.BotFactory.Authorization.Interfaces;

public interface IAuthorizationRepository
{
    Task SaveUserAsync(User user);
}