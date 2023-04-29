namespace Kyoto.Domain.Bot;

public interface IBotRepository
{
    Task<Guid> SaveAsync(long externalId, BotModel botModel);
    Task<List<string>> GetBotListAsync(long externalId);
    Task SetActiveBotAsync(long externalId, string name);
}