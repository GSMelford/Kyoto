using Kyoto.Domain.Telegram.Updates;
using Kyoto.Telegram.Receiver.Interfaces;

namespace Kyoto.Telegram.Receiver.Services;

public class UpdateService : IUpdateService
{
    private readonly ILogger<IUpdateService> _logger;
    private readonly IMessageDistributorService _messageDistributorService;

    public UpdateService(ILogger<IUpdateService> logger, IMessageDistributorService messageDistributorService)
    {
        _logger = logger;
        _messageDistributorService = messageDistributorService;
    }

    public async Task HandleAsync(Update update)
    {
        Guid sessionId = Guid.NewGuid();
        
        if (update.IsMessage()) {
            await _messageDistributorService.DefineAsync(sessionId, update.Message!);
            return;
        }
        
        _logger.LogInformation("The update did not find a handler and was ignored. UpdateId: {UpdateId}", update.UpdateId);
    }
}