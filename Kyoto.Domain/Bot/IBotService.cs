using Kyoto.Domain.System;

namespace Kyoto.Domain.Bot;

public interface IBotService
{
    Task<Guid> SaveAsync(Session session, string token);
    Task UpdateBotNameAsync(Guid botId, string name);
}