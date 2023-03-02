using Kyoto.Domain.Telegram.Updates;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Telegram.Receiver.Interfaces;

namespace Kyoto.Telegram.Receiver.Services;

public class UpdateService : IUpdateService
{
    private readonly ILogger<IUpdateService> _logger;
    private readonly IKafkaProducer<string> _kafkaProducer;

    public UpdateService(ILogger<IUpdateService> logger, IKafkaProducer<string> kafkaProducer)
    {
        _logger = logger;
        _kafkaProducer = kafkaProducer;
    }

    public async Task HandleAsync(Update update)
    {
        Guid updateSessionId = Guid.NewGuid();
        if (update.IsMessageExist())
        {
            await _kafkaProducer.ProduceAsync(new MessageEvent
            {
                SessionId = updateSessionId,
                Message = update.Message!
            });
            
            _logger.LogInformation("Update {SessionId} has been submitted for processing. " +
                                   "Updating the {UpdateType} Type", updateSessionId, nameof(MessageEvent));
        }
        
        _logger.LogInformation("The update did not find a handler and was ignored. Update id {UpdateId}", update.UpdateId);
    }
}