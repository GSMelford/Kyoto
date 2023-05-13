using Kyoto.Domain.PreparedMessage;

namespace Kyoto.Database.BotClient.Repositories.PreparedMessage;

public class PreparedMessageRepository : IPreparedMessageRepository
{
    private readonly IDatabaseContext _databaseContext;

    public PreparedMessageRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
}