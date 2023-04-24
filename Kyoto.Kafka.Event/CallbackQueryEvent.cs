using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Kafka.Event;

public class CallbackQueryEvent : BaseSessionEvent
{
    public CallbackQuery CallbackQuery { get; set; } = null!;
}