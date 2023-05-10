namespace Kyoto.Dal.BotFactory.Models;

public class MenuPanel : BaseModel
{
    public Guid ExternalUserId { get; set; }
    public ExternalUser ExternalUser { get; set; } = null!;
    public string? PreviousMenuItem { get; set; }
    public string CurrentMenuItem { get; set; } = null!;
}