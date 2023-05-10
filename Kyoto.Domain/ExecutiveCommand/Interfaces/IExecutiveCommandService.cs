using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.ExecutiveCommand.Interfaces;

public interface IExecutiveCommandService
{
    Task StartExecutiveCommandAsync(Session session, string commandName, object? additionalData = null);
    Task ProcessExecutiveCommandIfExistsAsync(
        Session session, 
        Message? message = null,
        CallbackQuery? callbackQuery = null);
}