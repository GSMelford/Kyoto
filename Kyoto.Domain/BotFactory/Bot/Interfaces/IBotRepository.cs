namespace Kyoto.Domain.BotFactory.Bot.Interfaces;

public interface IBotRepository
{
    Task<Guid> SaveAsync(long externalId, BotModel botModel);
    Task<List<string>> GetBotListAsync(long externalId, bool isEnable);
    Task SetEnableStatusBotAsync(long externalId, string name, bool isEnable);
}