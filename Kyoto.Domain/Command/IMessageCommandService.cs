using Kyoto.Domain.System;

namespace Kyoto.Domain.Command;

public interface IMessageCommandService
{
    ExecutiveCommandType ExecutiveCommandType { get; }
    Task ExecuteAsync(Session session, MessageCommandData messageCommandData);
}