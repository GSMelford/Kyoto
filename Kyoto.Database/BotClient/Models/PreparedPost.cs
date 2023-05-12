using Kyoto.Database.CommonModels;

namespace Kyoto.Database.BotClient.Models;

public class PreparedPost : BaseModel
{
    public Guid PostEventId { get; set; }
    public PostEvent PostEvent { get; set; } = null!;
    public string Message { get; set; } = null!;
}