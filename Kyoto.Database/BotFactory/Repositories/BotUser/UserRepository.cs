using Kyoto.Dal.BotFactory.Models;
using Kyoto.Domain.BotFactory.User.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Dal.BotFactory.Repositories.BotUser;

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