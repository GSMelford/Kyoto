using Kyoto.Domain.System;

namespace Kyoto.Domain.Command;

public interface IExecutiveCommandRepository
{
    Task<bool> SaveExecutiveCommandAsync(Session session, ExecutiveCommandType executiveCommand, object? additionalData = null);
    Task<bool> IsExecutiveCommandExistAsync(Session session);
    Task<ExecutiveCommand> PopExecutiveCommandAsync(Session session);
}