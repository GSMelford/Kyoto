using Kyoto.Domain.Command;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces;

public interface IExecutiveCommandService
{
    Task StartExecutiveCommandAsync(Session session, string commandName, object? additionalData = null);
    Task ProcessExecutiveCommandIfExistAsync(
        Session session, 
        Message? message = null,
        CallbackQuery? callbackQuery = null);
}