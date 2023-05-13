namespace Kyoto.Database.CommonModels;

public class TemplateMessage : BaseModel
{
    public Guid TemplateMessageTypeId { get; set; }
    public TemplateMessageType TemplateMessageType { get; set; } = null!;
    public string Text { get; set; } = null!;
}