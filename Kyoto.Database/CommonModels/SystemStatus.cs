namespace Kyoto.Database.CommonModels;

public class SystemStatus : BaseModel
{
    public string Name { get; set; } = null!;
    public string Status { get; set; } = null!;
}