using Kyoto.Domain.Telegram.Updates;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Telegram.Receiver.Interfaces;

namespace Kyoto.Telegram.Receiver.Services;

public class UpdateService : IUpdateService
{
    private readonly ILogger<IUpdateService> _logger;
    private readonly IKafkaProducer<string> _kafkaProducer;
    private readonly IMessageDistributorService _messageDistributorService;

    public UpdateService(ILogger<IUpdateService> logger, IMessageDistributorService messageDistributorService, IKafkaProducer<string> kafkaProducer)
    {
        _logger = logger;
        _messageDistributorService = messageDistributorService;
        _kafkaProducer = kafkaProducer;
    }

    public async Task HandleAsync(Update update)
    {
        Guid sessionId = Guid.NewGuid();
        
        if (update.IsMessage()) {
            await _messageDistributorService.DefineAsync(sessionId, update.Message!);
            return;
        }
        
        if (update.IsCallbackQuery())
        {
            await _kafkaProducer.ProduceAsync(new CallbackQueryEvent
            {
                CallbackQuery = update.CallbackQuery!,
                ChatId = update.CallbackQuery!.Message!.Chat.Id,
                ExternalUserId = update.CallbackQuery.From.Id,
                SessionId = sessionId
            });
            return;
        }
        
        _logger.LogInformation("The update did not find a handler and was ignored. UpdateId: {UpdateId}", update.UpdateId);
    }
}