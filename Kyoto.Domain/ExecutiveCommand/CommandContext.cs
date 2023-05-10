using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.ExecutiveCommand;

public class CommandContext
{
    public Session Session { get; private set; }
    public Message? Message { get; private set; }
    public CallbackQuery? CallbackQuery { get; private set; }
    public string? AdditionalData { get; private set; }
    public bool IsRetry { get; private set; }
    public bool IsInterrupt { get; private set; }
    public ExecutiveCommandStep? ToRetryStep { get; private set; }

    public CommandContext(
        Session session, 
        Message? message, 
        CallbackQuery? callbackQuery, 
        string? additionalData, 
        bool isRetry,
        ExecutiveCommandStep? toRetryStep,
        bool isInterrupt)
    {
        Session = session;
        Message = message;
        CallbackQuery = callbackQuery;
        AdditionalData = additionalData;
        IsRetry = isRetry;
        ToRetryStep = toRetryStep;
        IsInterrupt = isInterrupt;
    }

    public static CommandContext Create(
        Session session, 
        Message? message, 
        CallbackQuery? callbackQuery, 
        string? additionalData)
    {
        return new CommandContext(session, message, callbackQuery, additionalData, false, null, false);
    }

    public void SetAdditionalData(string additionalData)
    {
        AdditionalData = additionalData;
    }
    
    public void SetRetry(ExecutiveCommandStep? toStep = null)
    {
        IsRetry = true;
        ToRetryStep = toStep ?? ExecutiveCommandStep.FirstStep;
    }
    
    public void SetInterrupt()
    {
        IsInterrupt = true;
    }
}