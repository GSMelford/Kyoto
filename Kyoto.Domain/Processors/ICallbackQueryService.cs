using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Processors;

public interface ICallbackQueryService
{
    Task ProcessAsync(Session session, CallbackQuery callbackQuery);
}