namespace Kyoto.Infrastructure.Models;

public class TelegramUser : BaseModel
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public long TelegramId { get; set; }
    public string Username { get; set; } = null!;
}