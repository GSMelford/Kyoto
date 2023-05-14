using Kyoto.Database.CommonModels;
using Kyoto.Domain.BotFactory.Bot;
using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Database.BotFactory.Repositories.Bot;

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
            Token = botModel.Token,
            PrivateId = botModel.PrivateId,
            Username = botModel.Username,
            FirstName = botModel.FirstName,
            SupportsInlineQueries = botModel.SupportsInlineQueries,
            CanReadAllGroupMessages = botModel.CanReadAllGroupMessages,
            CanJoinGroups = botModel.CanJoinGroups,
            IsEnable = false,
            ExternalUser = await _databaseContext.Set<ExternalUser>().FirstAsync(x => x.PrivateId == externalId)
        };
        
        await _databaseContext.AddAsync(bot);
        await _databaseContext.SaveChangesAsync();
        return bot.Id;
    }
    
    public async Task<List<string>> GetBotListAsync(long externalId, bool isEnable)
    {
        return await _databaseContext.Set<Models.Bot>()
            .Include(x => x.ExternalUser)
            .Where(x => x.ExternalUser.PrivateId == externalId && x.IsEnable == isEnable)
            .Select(x => x.Username)
            .ToListAsync();
    }
    
    public async Task SetEnableStatusBotAsync(long externalId, string name, bool isEnable)
    {
        var bot = await GetBotAsync(externalId, name);
        bot!.IsEnable = isEnable;
        await _databaseContext.SaveChangesAsync();
    }

    private Task<Models.Bot?> GetBotAsync(long externalId, string name)
    {
        return _databaseContext.Set<Models.Bot>()
            .Include(x=>x.ExternalUser)
            .FirstOrDefaultAsync(x => x.ExternalUser.PrivateId == externalId && x.Username == name);
    }
}