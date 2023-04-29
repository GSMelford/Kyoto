using Kyoto.Domain.System;

namespace Kyoto.Domain.Bot;

public interface IBotService
{
    Task<Guid> SaveAsync(Session session, string name, string token);
}