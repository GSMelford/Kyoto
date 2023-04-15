using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Authorization;

public interface IAuthorizationService
{
    Task RegisterAsync(string username, Contact contact);
}