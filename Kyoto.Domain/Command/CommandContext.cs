using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Command;

public class CommandContext
{
    public Session Session { get; private set; }
    public Message? Message { get; private set; }
    public CallbackQuery? CallbackQuery { get; private set; }
    public string? AdditionalData { get; private set; }
    public bool IsRetry { get; private set; }
    public ExecutiveCommandStep? ToRetryStep { get; private set; }
    public bool IsFailure { get; private set; }
    public string? ErrorMessage { get; private set; }

    public CommandContext(
        Session session, 
        Message? message, 
        CallbackQuery? callbackQuery, 
        string? additionalData, 
        bool isRetry, 
        bool isFailure, 
        string? errorMessage, 
        ExecutiveCommandStep? toRetryStep)
    {
        Session = session;
        Message = message;
        CallbackQuery = callbackQuery;
        AdditionalData = additionalData;
        IsRetry = isRetry;
        IsFailure = isFailure;
        ErrorMessage = errorMessage;
        ToRetryStep = toRetryStep;
    }

    public static CommandContext Create(
        Session session, 
        Message? message, 
        CallbackQuery? callbackQuery, 
        string? additionalData)
    {
        return new CommandContext(session, message, callbackQuery, additionalData, false, false, null, null);
    }

    public void SetAdditionalData(string additionalData)
    {
        AdditionalData = additionalData;
    }
    
    public void SetRetry(ExecutiveCommandStep? toStep = null, string? errorMessage = null)
    {
        IsFailure = true;
        IsRetry = true;
        ErrorMessage = errorMessage;
        ToRetryStep = toStep;
    }
}