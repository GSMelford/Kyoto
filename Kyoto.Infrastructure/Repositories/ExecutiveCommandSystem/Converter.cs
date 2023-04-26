using Kyoto.Domain.Command;

namespace Kyoto.Infrastructure.Repositories.ExecutiveCommandSystem;

public static class Converter
{
    public static ExecutiveCommand ToDomain(this Models.ExecutiveCommand executiveCommand)
    {
        return ExecutiveCommand.Create(
            executiveCommand.SessionId,
            executiveCommand.ChatId,
            executiveCommand.ExternalUserId,
            Enum.Parse<CommandType>(executiveCommand.Command),
            executiveCommand.AdditionalData,
            (ExecutiveCommandStep)executiveCommand.Step,
            (CommandStepState)executiveCommand.StepState);
    }
}