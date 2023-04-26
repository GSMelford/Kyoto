using Kyoto.Domain.System;

namespace Kyoto.Domain.Menu;

public interface IMenuService
{
    Task DrawMenuIfExist(Session session, string value);
}