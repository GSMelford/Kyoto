using Kyoto.Domain.System;

namespace Kyoto.Domain.Command.GlobalCommand;

public interface IStartCommandService
{
    Task ExecuteAsync(Session session, string firstName);
}