using Kyoto.Domain.System;

namespace Kyoto.Domain.BotFactory.GlobalCommand.Interfaces;

public interface IStartCommandService
{
    Task ExecuteAsync(Session session, string firstName);
}