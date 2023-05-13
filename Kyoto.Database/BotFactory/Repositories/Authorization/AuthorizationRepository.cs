using Kyoto.Database.BotFactory.Models;
using Kyoto.Database.CommonModels;
using Kyoto.Domain.BotFactory.Authorization.Interfaces;
using User = Kyoto.Domain.BotFactory.Authorization.User;

namespace Kyoto.Database.BotFactory.Repositories.Authorization;

public class AuthorizationRepository : IAuthorizationRepository
{
    private readonly IDatabaseContext _databaseContext;

    public AuthorizationRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task SaveUserAsync(User user)
    {
        var userDal = new CommonModels.User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.PhoneNumber,
            TelegramUser = new ExternalUser
            {
                UserId = user.Id,
                Username = user.Username,
                PrivateId = user.TelegramId
            }
        };

        await _databaseContext.AddAsync(userDal);
        await _databaseContext.SaveChangesAsync();
    }
}