using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Command;

public interface IExecutiveCommandService
{
    Task HandleExecutiveCommandIfExistAsync(Session session, Message message);
    Task HandleExecutiveCommandIfExistAsync(Session session, CallbackQuery callbackQuery);
}