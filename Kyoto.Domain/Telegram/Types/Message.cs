namespace Kyoto.Domain.Telegram.Types;

public class Message
{
    public int MessageId { get; set; }
    public User? FromUser { get; set; }
    public Chat Chat { get; set; } = null!;
}