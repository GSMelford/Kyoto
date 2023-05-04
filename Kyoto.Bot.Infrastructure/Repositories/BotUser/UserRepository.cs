using Kyoto.Bot.Core.Database;
using Kyoto.Domain.BotUser;
using Kyoto.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Infrastructure.Repositories.BotUser;

public class UserRepository : IUserRepository
{
    private readonly IDatabaseContext _databaseContext;

    public UserRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<bool> IsUserExistAsync(long telegramId)
    {
        return await _databaseContext.Set<ExternalUser>()
            .FirstOrDefaultAsync(x => x.PrivateId == telegramId) is not null;
    }
}