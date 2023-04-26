using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Command;

public class CommandContext
{
    public Session Session { get; private set; }
    public Message? Message { get; private set; }
    public CallbackQuery? CallbackQuery { get; private set; }
    public object? AdditionalData { get; private set; }
    public bool IsRetry { get; private set; }

    private CommandContext(
        Session session, 
        Message? message,
        CallbackQuery? callbackQuery,
        string? additionalData,
        bool isRetry)
    {
        Session = session;
        Message = message;
        CallbackQuery = callbackQuery;
        AdditionalData = additionalData;
        IsRetry = isRetry;
    }

    public static CommandContext Create(
        Session session, 
        Message? message, 
        CallbackQuery? callbackQuery, 
        string? additionalData,
        bool isRetry = false)
    {
        return new CommandContext(session, message, callbackQuery, additionalData, isRetry);
    }

    public void SetAdditionalData(object value)
    {
        AdditionalData = value;
    }
}