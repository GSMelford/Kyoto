namespace Kyoto.Infrastructure.Models;

public class Bot : BaseModel
{
    public ExternalUser ExternalUser { get; set; } = null!;
    public string Prefix { get; set; } = null!;
    public string Token { get; set; } = null!;
}