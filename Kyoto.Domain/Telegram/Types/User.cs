namespace Kyoto.Domain.Telegram.Types;

public class User
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Username { get; set; }
}