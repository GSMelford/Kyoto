using Kyoto.Domain.System;

namespace Kyoto.Domain.BotFactory.Menu.Interfaces;

public interface IMenuService
{
    Task DrawMenuIfExist(Session session, string value);
}