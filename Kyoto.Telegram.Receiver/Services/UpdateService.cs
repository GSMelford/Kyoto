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

    public async Task HandleAsync(string tenantKey, Update update)
    {
        _logger.LogInformation("New update received. UpdateId: {UpdateId}", update.UpdateId);
        
        if (update.IsMessage()) 
        {
            await _messageDistributorService.DefineAsync(tenantKey, update.Message!);
        }
        
        if (update.IsCallbackQuery())
        {
            await _kafkaProducer.ProduceAsync(new CallbackQueryEvent(update.CallbackQuery!.ToSession(tenantKey))
            {
                CallbackQuery = update.CallbackQuery!
            }, tenantKey);
        }
    }
}