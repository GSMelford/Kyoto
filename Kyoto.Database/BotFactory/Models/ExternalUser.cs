namespace Kyoto.Dal.BotFactory.Models;

public class ExternalUser : BaseModel
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public long PrivateId { get; set; }
    public string Username { get; set; } = null!;
    
    public MenuPanel? MenuPanel { get; set; }
    public ICollection<Bot> Bots { get; set; } = new List<Bot>();
}