using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Command;

public interface IExecutiveCommandService
{
    Task StartExecutiveCommandAsync(Session session, CommandType commandType, object? additionalData = null);
    Task ProcessExecutiveCommandIfExistAsync(
        Session session, 
        Message? message = null,
        CallbackQuery? callbackQuery = null);
}