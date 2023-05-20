namespace Kyoto.Database.CommonModels;

public class PreparedMessage : BaseModel
{
    public Guid PostEventId { get; set; }
    public PostEvent PostEvent { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime? TimeToSend { get; set; }
    public string? KeyWords { get; set; }
}