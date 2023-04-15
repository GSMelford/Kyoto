namespace Kyoto.Infrastructure.Models;

public class ExecutiveTelegramCommand : BaseModel
{
    public long TelegramId { get; set; }
    public string Command { get; set; } = null!;
}