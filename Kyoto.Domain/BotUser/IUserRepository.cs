namespace Kyoto.Domain.BotUser;

public interface IUserRepository
{
    Task<bool> IsUserExistAsync(long telegramId);
}