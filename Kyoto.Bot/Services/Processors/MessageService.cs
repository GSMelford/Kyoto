using Kyoto.Domain.ExecutiveCommand;
using Kyoto.Domain.Processors;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Bot.Services.Processors;

public class MessageService : IMessageService
{
    private readonly IExecutiveTelegramCommandService _executiveTelegramCommandService;

    public MessageService(IExecutiveTelegramCommandService executiveTelegramCommandService)
    {
        _executiveTelegramCommandService = executiveTelegramCommandService;
    }

    public Task ProcessAsync(Guid sessionId, Message message)
    {
        return _executiveTelegramCommandService.HandleExecutiveCommandIfExistAsync(sessionId, message);
    }
}