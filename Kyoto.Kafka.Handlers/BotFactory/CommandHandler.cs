using Kyoto.Domain.BotFactory.GlobalCommand;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kyoto.Kafka.Handlers.BotFactory;

public class CommandHandler : IKafkaHandler<CommandEvent>
{
    private readonly ILogger<IKafkaHandler<CommandEvent>> _logger;
    private readonly IKafkaProducer<string> _kafkaProducer;

    public CommandHandler(ILogger<IKafkaHandler<CommandEvent>> logger, IKafkaProducer<string> kafkaProducer)
    {
        _logger = logger;
        _kafkaProducer = kafkaProducer;
    }

    public async Task HandleAsync(CommandEvent commandEvent)
    {
        var session = commandEvent.GetSession();
        if (commandEvent.GlobalCommandType == GlobalCommandType.Start)
        {
            await _kafkaProducer.ProduceAsync(new StartCommandEvent (session)
            {
                UserFirstName = commandEvent.Message.FromUser!.FirstName
            }, session.TenantKey);
            
            _logger.LogInformation("Start command in progress. SessionId: {SessionId}", commandEvent.SessionId);
            return;
        }
        
        _logger.LogInformation("Command not found. End of session. SessionId: {SessionId}", commandEvent.SessionId);
    }
}