using Kyoto.Domain.CommandPerformance;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.KafkaHandlers;

public class CommandHandler : IEventHandler<CommandEvent>
{
    private readonly ILogger<IEventHandler<CommandEvent>> _logger;
    private readonly IKafkaProducer<string> _kafkaProducer;

    public CommandHandler(ILogger<IEventHandler<CommandEvent>> logger, IKafkaProducer<string> kafkaProducer)
    {
        _logger = logger;
        _kafkaProducer = kafkaProducer;
    }

    public async Task HandleAsync(CommandEvent commandEvent)
    {
        if (commandEvent.Command == Command.Start)
        {
            await _kafkaProducer.ProduceAsync(new StartCommandEvent
            {
                SessionId = commandEvent.SessionId,
                ChatId = commandEvent.Message.Chat.Id,
                TelegramUserId = commandEvent.Message.FromUser!.Id
            });
            
            _logger.LogInformation("Start command in progress. SessionId: {SessionId}", commandEvent.SessionId);
            return;
        }
        
        _logger.LogInformation("Command not found. End of session. SessionId: {SessionId}", commandEvent.SessionId);
    }
}