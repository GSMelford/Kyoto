using Kyoto.Domain.Authorization;
using Kyoto.Infrastructure.Models;
using User = Kyoto.Domain.Authorization.User;

namespace Kyoto.Infrastructure.Repositories.Authorization;

public class AuthorizationRepository : IAuthorizationRepository
{
    private readonly IDatabaseContext _databaseContext;

    public AuthorizationRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task SaveUserAsync(User user)
    {
        var userDal = new Models.User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Phone = user.PhoneNumber,
            TelegramUser = new TelegramUser
            {
                UserId = user.Id,
                Username = user.Username,
                TelegramId = user.TelegramId
            }
        };

        await _databaseContext.AddAsync(userDal);
        await _databaseContext.SaveChangesAsync();
    }
}