using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Processors.Interfeces;

public interface ICallbackQueryService
{
    Task ProcessAsync(Session session, CallbackQuery callbackQuery);
}