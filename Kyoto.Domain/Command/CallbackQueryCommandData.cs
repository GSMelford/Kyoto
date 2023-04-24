using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Command;

public class CallbackQueryCommandData
{
    public CallbackQuery CallbackQuery { get; private set; }
    public object? AdditionalData { get; private set; }

    private CallbackQueryCommandData(CallbackQuery callbackQuery, object? additionalData)
    {
        CallbackQuery = callbackQuery;
        AdditionalData = additionalData;
    }

    public static CallbackQueryCommandData Create(CallbackQuery callbackQuery, object? additionalData)
    {
        return new CallbackQueryCommandData(callbackQuery, additionalData);
    }
}