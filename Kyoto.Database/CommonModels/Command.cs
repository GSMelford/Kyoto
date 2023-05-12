namespace Kyoto.Database.CommonModels;

public class Command : BaseModel
{
    public Guid SessionId { get; set; }
    public long ChatId { get; set; }
    public string Name { get; set; } = null!;
    public long ExternalUserId { get; set; }
    public int Step { get; set; }
    public int State { get; set; }
    public string? AdditionalData { get; set; }
}