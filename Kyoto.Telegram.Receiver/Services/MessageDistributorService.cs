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

    public async Task DefineAsync(string tenantKey, Message message)
    {
        var session = message.ToSession(tenantKey);
        if (message.MessageEntities is null)
        {
            await _kafkaProducer.ProduceAsync(new MessageEvent (session)
            {
                Message = message
            });
            
            _logger.LogInformation("Update has been submitted for processing. " +
                                   "SessionId: {SessionId}, EventType: {UpdateType}", session.Id, nameof(MessageEvent));
            return;
        }
        
        if (message.TryGetCommand(out var command))
        {
            await _kafkaProducer.ProduceAsync(new CommandEvent (session)
            {
                Message = message,
                GlobalCommandType = command!.Value
            });
            
            _logger.LogInformation("Update has been submitted for processing. " +
                                   "SessionId: {SessionId}, EventType: {UpdateType}", session.Id, nameof(CommandEvent));
            return;
        }
        
        _logger.LogInformation("Message handler not defined. End of session. SessionId: {SessionId}", session.Id);
    }
}