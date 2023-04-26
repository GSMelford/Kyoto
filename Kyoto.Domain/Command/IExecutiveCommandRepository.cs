using Kyoto.Domain.System;

namespace Kyoto.Domain.Command;

public interface IExecutiveCommandRepository
{
    Task SaveAsync(Session session, CommandType command, object? additionalData = null);
    Task UpdateAsync(Session session, ExecutiveCommand executiveCommand);
    Task<bool> IsExistAsync(Session session);
    Task RemoveAsync(Session session);
    Task<ExecutiveCommand> GetAsync(Session session);
}