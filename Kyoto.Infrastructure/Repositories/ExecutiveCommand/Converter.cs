using Kyoto.Domain.ExecutiveCommand;

namespace Kyoto.Infrastructure.Repositories.ExecutiveCommand;

public static class Converter
{
    public static ExecutiveTelegramCommand ToDomain(this Models.ExecutiveTelegramCommand executiveTelegramCommand)
    {
        return ExecutiveTelegramCommand.Create(
            executiveTelegramCommand.TelegramId,
            Enum.Parse<ExecutiveCommandType>(executiveTelegramCommand.Command));
    }
}