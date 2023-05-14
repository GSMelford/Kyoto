using Kyoto.Domain.System;

namespace Kyoto.Domain.TemplateMessage;

public interface ITemplateMessageService
{
    Task SendTemplateMessageAsync(Session session, TemplateMessageTypeValue type);
    Task UpdateTemplateMessageAsync(TemplateMessageTypeValue type, string newText);
}