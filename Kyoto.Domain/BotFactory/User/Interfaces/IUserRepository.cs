namespace Kyoto.Domain.BotFactory.User.Interfaces;

public interface IUserRepository
{
    Task<bool> IsUserExistAsync(long telegramId);
}