using Kyoto.Domain.System;

namespace Kyoto.Domain.Bot;

public interface IBotService
{
    Task ActivateBotAsync(Session session, string username);
    Task<Guid> SaveAsync(Session session, BotModel botModel);
}