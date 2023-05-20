using Kyoto.Domain.PreparedMessagesSystem;

namespace Kyoto.Database.CommonModels;

public class PostEvent : BaseModel
{
    public string Name { get; set; } = null!;
    public PostEventCode Code { get; set; }
}