using Kyoto.Domain.ExecutiveCommand;
using ExecutiveCommand = Kyoto.Dal.CommonModels.ExecutiveCommand;

namespace Kyoto.Dal.CommonRepositories.ExecuteCommandSystem;

public static class Converter
{
    public static Domain.ExecutiveCommand.ExecutiveCommand ToDomain(this ExecutiveCommand executiveCommand)
    {
        return Domain.ExecutiveCommand.ExecutiveCommand.Create(
            executiveCommand.SessionId,
            executiveCommand.ChatId,
            executiveCommand.ExternalUserId,
            executiveCommand.Command,
            executiveCommand.AdditionalData,
            (ExecutiveCommandStep)executiveCommand.Step,
            (CommandStepState)executiveCommand.StepState);
    }
}