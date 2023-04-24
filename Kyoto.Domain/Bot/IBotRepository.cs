namespace Kyoto.Domain.Bot;

public interface IBotRepository
{
    Task<Guid> SaveAsync(long externalId, BotModel botModel);
    Task UpdateNameAsync(Guid botId, string prefix);
}