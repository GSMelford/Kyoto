namespace Kyoto.Infrastructure.Models;

public class ExecutiveCommand : BaseModel
{
    public Guid SessionId { get; set; }
    public long ChatId { get; set; }
    public long ExternalUserId { get; set; }
    public string Command { get; set; } = null!;
    public string? AdditionalData { get; set; }
}