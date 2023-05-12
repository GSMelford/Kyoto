using Kyoto.Domain.System;

namespace Kyoto.Domain.CommandSystem.Interfaces;

public interface ICommandRepository
{
    Task<bool> TrySaveCommandAsync(Session session, string commandName);
    Task UpdateCommandAsync(Session session, Command command);
    Task<bool> IsCommandExistsAsync(Session session);
    Task RemoveAsync(Session session);
    Task<Command> GetAsync(Session session);
}