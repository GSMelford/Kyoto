using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Domain.Telegram.Updates;

public class Update
{
    public int UpdateId { get; set; }
    public Message? Message { get; set; }
    public CallbackQuery? CallbackQuery { get; set; }

    public bool IsMessage()
    {
        return Message is not null;
    }
    
    public bool IsCallbackQuery()
    {
        return CallbackQuery is not null;
    }
}