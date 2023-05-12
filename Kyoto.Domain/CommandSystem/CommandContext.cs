using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.CommandSystem;

public class CommandContext
{
    public Message? Message { get; private set; }
    public CallbackQuery? CallbackQuery { get; private set; }
    public string? AdditionalData { get; private set; }

    private CommandContext(
        Message? message, 
        CallbackQuery? callbackQuery, 
        string? additionalData = null)
    {
        Message = message;
        CallbackQuery = callbackQuery;
        AdditionalData = additionalData;
    }
    
    public static CommandContext Create(
        Message? message,
        CallbackQuery? callbackQuery)
    {
        return new CommandContext(message, callbackQuery);
    }

    public void SetAdditionalData(string? additionalData = null)
    {
        AdditionalData = additionalData;
    }
}