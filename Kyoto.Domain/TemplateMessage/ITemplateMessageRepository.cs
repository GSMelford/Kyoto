namespace Kyoto.Domain.TemplateMessage;

public interface ITemplateMessageRepository
{
    Task<TemplateMessage> GetAsync(TemplateMessageTypeValue templateMessageTypeValue);

    Task UpdateAsync(TemplateMessageTypeValue templateMessageTypeValue, string newText);
}