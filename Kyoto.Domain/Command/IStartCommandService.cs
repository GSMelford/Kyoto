using Kyoto.Domain.System;

namespace Kyoto.Domain.Command;

public interface IStartCommandService
{
    Task ExecuteAsync(Session session);
}