using Kyoto.Domain.System;
using Kyoto.Domain.TemplateMessage;

namespace Kyoto.Services.BotClient.TemplateMessage;

public class TemplateMessageService : ITemplateMessageService
{
    private readonly ITemplateMessageRepository _templateMessageRepository;

    public TemplateMessageService(ITemplateMessageRepository templateMessageRepository)
    {
        _templateMessageRepository = templateMessageRepository;
    }

    public Task<Domain.TemplateMessage.TemplateMessage> GetTemplateMessageAsync(TemplateMessageTypeValue type)
    {
        return _templateMessageRepository.GetAsync(type);
    }

    public async Task UpdateTemplateMessageAsync(TemplateMessageTypeValue type, string newText)
    {
        await _templateMessageRepository.UpdateAsync(type, newText);
    }
}