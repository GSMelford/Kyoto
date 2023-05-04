using Kyoto.Domain.System;
using ExecutiveCommand = Kyoto.Bot.Core.ExecutiveCommandSystem.Models.ExecutiveCommand;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces;

public interface IExecutiveCommandRepository
{
    Task SaveAsync(Session session, string commandName, object? additionalData = null);
    Task UpdateAsync(Session session, ExecutiveCommand executiveCommand);
    Task<bool> IsExistAsync(Session session);
    Task RemoveAsync(Session session);
    Task<ExecutiveCommand> GetAsync(Session session);
}