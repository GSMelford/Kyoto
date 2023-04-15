namespace Kyoto.Domain.Telegram.Types;

public class Contact
{
    public string PhoneNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? LastName { get; set; }
    public long UserId { get; set; }
    public string? Vcard { get; set; }
}