namespace Kyoto.Domain.ExecutiveCommand;

public interface IExecutiveTelegramCommandRepository
{
    Task<bool> SaveExecutiveCommandAsync(long telegramId, ExecutiveCommandType executiveCommand);
    Task<bool> IsExecutiveCommandExistAsync(long telegramId);
    Task<ExecutiveTelegramCommand> PopExecutiveCommandAsync(long telegramId);
}