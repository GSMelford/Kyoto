namespace Kyoto.Domain.BotFactory.Authorization.Interfaces;

public interface IAuthorizationService
{
    Task RegisterAsync(User user);
}