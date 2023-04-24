using Kyoto.Domain.System;

namespace Kyoto.Domain.Command;

public interface ICallbackQueryCommandService
{
    ExecutiveCommandType ExecutiveCommandType { get; }
    Task ExecuteAsync(Session session, CallbackQueryCommandData callbackQueryCommandData);
}