namespace Kyoto.Domain.BotFactory.Bot.Interfaces;

public interface IBotRepository
{
    Task<Guid> SaveAsync(long externalId, BotModel botModel);
    Task<List<string>> GetBotsAsync(long externalId, bool isEnable);
    Task SetBotStatusesAsync(long externalId, string name, bool isEnable, bool? isDeployed = null);
    Task<List<string>> GetDeployedBotsAsync(long externalId);
}