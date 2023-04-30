using Kyoto.Domain.System;
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
        if (update.IsMessage()) {
            await _messageDistributorService.DefineAsync(tenantKey, update.Message!);
            return;
        }
        
        if (update.IsCallbackQuery())
        {
            await _kafkaProducer.ProduceAsync(new CallbackQueryEvent(update.CallbackQuery!.ToSession(tenantKey))
            {
                CallbackQuery = update.CallbackQuery!
            });
            return;
        }
        
        _logger.LogInformation("The update did not find a handler and was ignored. UpdateId: {UpdateId}", update.UpdateId);
    }
}