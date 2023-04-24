using Kyoto.Domain.Telegram.Types;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Telegram.Receiver.Interfaces;

namespace Kyoto.Telegram.Receiver.Services;

public class MessageDistributorService : IMessageDistributorService
{
    private readonly ILogger<IMessageDistributorService> _logger;
    private readonly IKafkaProducer<string> _kafkaProducer;

    public MessageDistributorService(ILogger<IMessageDistributorService> logger, IKafkaProducer<string> kafkaProducer)
    {
        _logger = logger;
        _kafkaProducer = kafkaProducer;
    }

    public async Task DefineAsync(Guid sessionId, Message message)
    {
        if (message.MessageEntities is null)
        {
            await _kafkaProducer.ProduceAsync(new MessageEvent
            {
                SessionId = sessionId,
                Message = message
            });
            
            _logger.LogInformation("Update has been submitted for processing. " +
                                   "SessionId: {SessionId}, EventType: {UpdateType}", sessionId, nameof(MessageEvent));
            return;
        }
        
        if (message.TryGetCommand(out var command))
        {
            await _kafkaProducer.ProduceAsync(new CommandEvent
            {
                SessionId = sessionId,
                Message = message,
                CommandType = command!.Value
            });
            
            _logger.LogInformation("Update has been submitted for processing. " +
                                   "SessionId: {SessionId}, EventType: {UpdateType}", sessionId, nameof(CommandEvent));
            return;
        }
        
        _logger.LogInformation("Message handler not defined. End of session. SessionId: {SessionId}", sessionId);
    }
}