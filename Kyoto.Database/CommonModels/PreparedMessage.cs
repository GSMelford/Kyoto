namespace Kyoto.Database.CommonModels;

public class PreparedMessage : BaseModel
{
    public Guid PostEventId { get; set; }
    public PostEvent PostEvent { get; set; } = null!;
    public string Message { get; set; } = null!;
}