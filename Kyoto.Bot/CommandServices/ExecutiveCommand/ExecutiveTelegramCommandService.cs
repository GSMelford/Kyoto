using Kyoto.Domain.ExecutiveCommand;
using Kyoto.Domain.Telegram.Types;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Bot.CommandServices.ExecutiveCommand;

public class ExecutiveTelegramCommandService : IExecutiveTelegramCommandService
{
    private readonly IKafkaProducer<string> _kafkaProducer;
    private readonly IExecutiveTelegramCommandRepository _executiveTelegramCommandRepository;

    public ExecutiveTelegramCommandService(
        IKafkaProducer<string> kafkaProducer, 
        IExecutiveTelegramCommandRepository executiveTelegramCommandRepository)
    {
        _kafkaProducer = kafkaProducer;
        _executiveTelegramCommandRepository = executiveTelegramCommandRepository;
    }
    
    public async Task HandleExecutiveCommandIfExistAsync(Guid sessionId, Message message)
    {
        var telegramId = message.FromUser?.Id;
        if (telegramId is null) {
            throw new Exception();
        }
        
        if (await _executiveTelegramCommandRepository.IsExecutiveCommandExistAsync(telegramId.Value))
        {
            var command = await _executiveTelegramCommandRepository.PopExecutiveCommandAsync(telegramId.Value);
            if (command.ExecutiveCommand == ExecutiveCommandType.Register)
            {
                await _kafkaProducer.ProduceAsync(new RegisterEvent
                {
                    Contact = message.Contact!,
                    ChatId = message.Chat.Id,
                    Username = message.FromUser!.Username!,
                    SessionId = sessionId
                });
            }
        }
    }
}