using Kyoto.Bot.Core.Database;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem.Models;

public class ExecutiveCommandDal : BaseModel
{
    public Guid SessionId { get; set; }
    public long ChatId { get; set; }
    public string Command { get; set; } = null!;
    public long ExternalUserId { get; set; }
    public int Step { get; set; }
    public int StepState { get; set; }
    public string? AdditionalData { get; set; }
}