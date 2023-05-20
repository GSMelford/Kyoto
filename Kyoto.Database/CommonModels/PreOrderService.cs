namespace Kyoto.Database.CommonModels;

public class PreOrderService : BaseModel
{
    public Guid ServiceId { get; set; }
    public Service Service { get; set; } = null!;
    public Guid ExternalUserId { get; set; }
    public ExternalUser ExternalUser { get; set; } = null!;
    public string? Comment { get; set; } = null!;
}