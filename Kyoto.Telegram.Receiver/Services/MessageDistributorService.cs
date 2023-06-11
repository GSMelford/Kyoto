using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Telegram.Types;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Telegram.Receiver.Interfaces;

namespace Kyoto.Telegram.Receiver.Services;

public class MessageDistributorService : IMessageDistributorService
{
    private readonly IKafkaProducer<string> _kafkaProducer;

    public MessageDistributorService(IKafkaProducer<string> kafkaProducer)
    {
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
                Name = ConvertCommand(command!)
            }, tenantKey);
        }
        else
        {
            await _kafkaProducer.ProduceAsync(new MessageEvent(session)
            {
                Message = message
            }, tenantKey);
        }
    }

    private static string ConvertCommand(string command)
    {
        return command switch
        {
            "/start" => CommandCodes.Registration,
            "/cancel" => CommandCodes.Cancel,
            _ => string.Empty
        };
    }
}