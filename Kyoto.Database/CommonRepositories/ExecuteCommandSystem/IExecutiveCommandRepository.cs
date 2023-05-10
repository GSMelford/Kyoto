using Kyoto.Domain.System;
using ExecutiveCommand = Kyoto.Domain.ExecutiveCommand.ExecutiveCommand;

namespace Kyoto.Dal.CommonRepositories.ExecuteCommandSystem;

public interface IExecutiveCommandRepository
{
    Task SaveAsync(Session session, string commandName, object? additionalData = null);
    Task UpdateAsync(Session session, ExecutiveCommand executiveCommand);
    Task<bool> IsExistsAsync(Session session);
    Task RemoveAsync(Session session);
    Task<ExecutiveCommand> GetAsync(Session session);
}