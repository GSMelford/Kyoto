namespace Kyoto.Database.CommonModels;

public class Service : BaseModel
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
}