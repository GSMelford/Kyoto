using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Telegram.Receiver.Services;

public static class Converter
{
    public static Session ToSession(this CallbackQuery callbackQuery, string tenantKey)
    {
        return Session.CreateNew(
            callbackQuery.Message!.Chat.Id,
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            tenantKey);
    }
    
    public static Session ToSession(this Message message, string tenantKey)
    {
        return Session.CreateNew(
            message.Chat.Id,
            message.FromUser!.Id,
            message.MessageId,
            tenantKey);
    }
}