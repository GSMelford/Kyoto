using Kyoto.Domain.TemplateMessage;

namespace Kyoto.Dto.TemplateMessageSystem;

public static class Converter
{
    public static TemplateMessageDto ToDto(this TemplateMessage templateMessage)
    {
        return new TemplateMessageDto
        {
            Text = templateMessage.Text,
            Code = templateMessage.Code,
            Description = templateMessage.Description
        };
    }
}