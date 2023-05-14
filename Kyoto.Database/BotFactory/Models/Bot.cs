using Kyoto.Database.CommonModels;

namespace Kyoto.Database.BotFactory.Models;

public class Bot : BaseModel
{
    public ExternalUser ExternalUser { get; set; } = null!;
    public string PrivateId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public bool CanJoinGroups { get; set; }
    public bool CanReadAllGroupMessages { get; set; }
    public bool SupportsInlineQueries { get; set; }
    public string Token { get; set; } = null!;
    public bool IsEnable { get; set; }
    public bool IsDeployed { get; set; }
}