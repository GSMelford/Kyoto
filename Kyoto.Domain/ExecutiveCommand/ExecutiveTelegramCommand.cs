namespace Kyoto.Domain.ExecutiveCommand;

public class ExecutiveTelegramCommand
{
    public long TelegramId { get; set; }
    public ExecutiveCommandType ExecutiveCommand { get; set; }

    private ExecutiveTelegramCommand(long telegramId, ExecutiveCommandType executiveCommand)
    {
        TelegramId = telegramId;
        ExecutiveCommand = executiveCommand;
    }

    public static ExecutiveTelegramCommand Create(long telegramId, ExecutiveCommandType executiveCommand)
    {
        return new ExecutiveTelegramCommand(telegramId, executiveCommand);
    }
}