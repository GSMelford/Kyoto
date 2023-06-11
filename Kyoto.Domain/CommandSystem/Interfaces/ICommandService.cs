using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.CommandSystem.Interfaces;

public interface ICommandService
{
    Task<string> CancelCommandAsync(Session session);
    public Task<bool> ProcessCommandAsync(
        Session session,
        string commandName,
        Message? message = null,
        CallbackQuery? callbackQuery = null);
}