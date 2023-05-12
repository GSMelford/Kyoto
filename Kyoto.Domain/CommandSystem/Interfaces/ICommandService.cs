using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.CommandSystem.Interfaces;

public interface ICommandService
{
    Task ProcessCommandAsync(Session session, CallbackQuery callbackQuery);
    Task ProcessCommandAsync(Session session, Message message);

    public Task ProcessCommandAsync(
        Session session,
        string commandName,
        Message? message = null,
        CallbackQuery? callbackQuery = null);
}