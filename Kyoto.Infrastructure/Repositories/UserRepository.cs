using Kyoto.Domain.BotUser;
using Kyoto.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDatabaseContext _databaseContext;

    public UserRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<bool> IsUserExistAsync(long telegramId)
    {
        return await _databaseContext.Set<TelegramUser>()
            .FirstOrDefaultAsync(x => x.TelegramId == telegramId) is not null;
    }
}