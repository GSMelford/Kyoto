namespace Kyoto.Database.CommonModels;

public class Feedback : BaseModel
{
    public Guid? ExternalUserId { get; set; }
    public ExternalUser? ExternalUser { get; set; }
    public string Text { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int StarCount { get; set; }
}