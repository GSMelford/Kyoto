using Kyoto.Bot.Core.Database;
using Kyoto.Domain.Bot;
using Kyoto.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using BotDal = Kyoto.Infrastructure.Models.Bot;

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
    
    public async Task<List<string>> GetBotListAsync(long externalId)
    {
        return await _databaseContext.Set<Models.Bot>()
            .Include(x => x.ExternalUser)
            .Where(x => x.ExternalUser.PrivateId == externalId)
            .Select(x => x.Username)
            .ToListAsync();
    }

    public async Task SetActiveBotAsync(long externalId, string name)
    {
        var bot = await GetBotAsync(externalId, name);
        bot!.IsEnable = true;
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<bool> IsBotActiveAsync(long externalId, string name)
    {
        var bot = await GetBotAsync(externalId, name);
        return bot!.IsEnable;
    }

    private Task<BotDal?> GetBotAsync(long externalId, string name)
    {
        return _databaseContext.Set<BotDal>()
            .Include(x=>x.ExternalUser)
            .FirstOrDefaultAsync(x => x.ExternalUser.PrivateId == externalId && x.Username == name);
    }
}