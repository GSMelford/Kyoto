namespace Kyoto.Infrastructure.Models;

public class User : BaseModel
{
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string Phone { get; set; } = null!;

    public ExternalUser? TelegramUser { get; set; }
}