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
        if (message.TryGetCommand(out var command))
        {
            await _kafkaProducer.ProduceAsync(new CommandEvent(session)
            {
                Message = message,
                GlobalCommandType = command!.Value
            }, tenantKey);
        }

        await _kafkaProducer.ProduceAsync(new MessageEvent(session)
        {
            Message = message
        }, tenantKey);
    }
}