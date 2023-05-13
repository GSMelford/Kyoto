namespace Kyoto.Database.CommonModels;

public class TemplateMessageType : BaseModel
{
    public string Name { get; set; } = null!;
    public int Code { get; set; }
    public string Description { get; set; } = null!;
}