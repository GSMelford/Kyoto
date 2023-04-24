using Kyoto.Domain.Bot;
using Kyoto.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Infrastructure.Repositories.Bot;

public class BotRepository : IBotRepository
{
    private readonly IDatabaseContext _databaseContext;

    public BotRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Guid> SaveAsync(long externalId, BotModel botModel)
    {
        var bot = new Models.Bot
        {
            Id = botModel.Id,
            Prefix = botModel.Prefix,
            Token = botModel.Token,
            ExternalUser = await _databaseContext.Set<ExternalUser>().FirstAsync(x => x.PrivateId == externalId)
        };
        
        await _databaseContext.AddAsync(bot);
        await _databaseContext.SaveChangesAsync();
        return bot.Id;
    }

    public async Task UpdateNameAsync(Guid botId, string prefix)
    {
        var bot = await _databaseContext.Set<Models.Bot>().FirstOrDefaultAsync(x => x.Id == botId);
        
        if (bot is not null)
        {
            bot.Prefix = prefix;
            _databaseContext.Update(bot);
            await _databaseContext.SaveChangesAsync();
        }
    }
}