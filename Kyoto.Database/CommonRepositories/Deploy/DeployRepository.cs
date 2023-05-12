using Kyoto.Database.BotClient.Models;
using Kyoto.Domain.BotClient.Deploy.Interfaces;

namespace Kyoto.Database.CommonRepositories.Deploy;

public class DeployRepository : IDeployRepository
{
    private readonly IDatabaseContext _databaseContext;

    public DeployRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task InitDatabaseAsync()
    {
        var postEvent = new PostEvent { Name = "Start" };
        await _databaseContext.AddAsync(new PreparedPost
        {
            PostEvent = postEvent,
            Message = "Hello!"
        });

        await _databaseContext.SaveChangesAsync();
    }
}