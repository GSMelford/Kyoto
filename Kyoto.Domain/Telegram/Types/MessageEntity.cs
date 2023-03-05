namespace Kyoto.Domain.Telegram.Types;

public class MessageEntity
{
    public MessageEntityType Type { get; set; } = null!;
    public int Offset { get; set; }
    public int Length { get; set; }
    public string? Url { get; set; }
    public User? User { get; set; }
    public string? Language { get; set; }
    public string? CustomEmojiId { get; set; }
}