using Kyoto.Domain.TemplateMessage;

namespace Kyoto.Dto.TemplateMessageSystem;

public class TemplateMessageDto
{
    public TemplateMessageTypeValue Code { get; set; }
    public string Text { get; set; } = null!;
    public string Description { get; set; } = null!;
}