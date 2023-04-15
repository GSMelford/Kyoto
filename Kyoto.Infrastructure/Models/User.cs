namespace Kyoto.Infrastructure.Models;

public class User : BaseModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public Guid TelegramUserId { get; set; }
    public TelegramUser TelegramUser { get; set; } = null!;
}