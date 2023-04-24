using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Command;

public class MessageCommandData
{
    public Message Message { get; private set; }
    public object? AdditionalData { get; private set; }

    private MessageCommandData(Message message, object? additionalData)
    {
        Message = message;
        AdditionalData = additionalData;
    }

    public static MessageCommandData Create(Message message, object? additionalData)
    {
        return new MessageCommandData(message, additionalData);
    }
}