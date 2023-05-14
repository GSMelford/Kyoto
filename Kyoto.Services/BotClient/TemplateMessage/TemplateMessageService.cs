using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Services.BotFactory.PostSystem;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Services.BotClient.TemplateMessage;

public class TemplateMessageService : ITemplateMessageService
{
    private readonly IPostService _postService;
    private readonly ITemplateMessageRepository _templateMessageRepository;

    public TemplateMessageService(IPostService postService, ITemplateMessageRepository templateMessageRepository)
    {
        _postService = postService;
        _templateMessageRepository = templateMessageRepository;
    }

    public async Task SendTemplateMessageAsync(Session session, TemplateMessageTypeValue type)
    {
        var templateMessage = await _templateMessageRepository.GetAsync(type);
        await _postService.PostBehalfOfFactoryAsync(session, new SendMessageRequest(new SendMessageParameters
        {
            Text = $"Message type: {Enum.GetName(templateMessage.Code)}.\n" +
                   $"Message description: {templateMessage.Description}\n" +
                   $"Text: {templateMessage.Text}",
            ChatId = session.ChatId
        }).ToRequest());
    }

    public async Task UpdateTemplateMessageAsync(TemplateMessageTypeValue type, string newText)
    {
        await _templateMessageRepository.UpdateAsync(type, newText);
    }
}