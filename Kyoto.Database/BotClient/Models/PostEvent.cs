using Kyoto.Database.CommonModels;

namespace Kyoto.Database.BotClient.Models;

public class PostEvent : BaseModel
{
    public string Name { get; set; } = null!;
}