namespace Kyoto.Domain.TemplateMessage;

public interface ITemplateMessageService
{
    Task<TemplateMessage> GetTemplateMessageAsync(TemplateMessageTypeValue type);
    Task UpdateTemplateMessageAsync(TemplateMessageTypeValue type, string newText);
}