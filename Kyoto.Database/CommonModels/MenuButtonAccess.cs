namespace Kyoto.Database.CommonModels;

public class MenuButtonAccess : BaseModel
{
    public Guid ExternalUserId { get; set; }
    public ExternalUser ExternalUser { get; set; } = null!;
    
    public Guid MenuButtonId { get; set; }
    public MenuButton MenuButton { get; set; } = null!;
}