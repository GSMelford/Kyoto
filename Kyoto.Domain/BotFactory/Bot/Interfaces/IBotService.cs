using Kyoto.Domain.System;

namespace Kyoto.Domain.BotFactory.Bot.Interfaces;

public interface IBotService
{
    Task ActivateBotAsync(Session session, string username);
    Task<Guid> SaveAsync(Session session, BotModel botModel);
}