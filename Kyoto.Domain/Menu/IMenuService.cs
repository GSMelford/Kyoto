using Kyoto.Domain.System;

namespace Kyoto.Domain.Menu;

public interface IMenuService
{
    Task ExecuteIfCommandAsync(Session session, string value);
}