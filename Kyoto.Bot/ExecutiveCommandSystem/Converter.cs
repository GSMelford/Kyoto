using Kyoto.Bot.Core.ExecutiveCommandSystem.Models;
using Kyoto.Domain.Command;
using ExecutiveCommand = Kyoto.Bot.Core.ExecutiveCommandSystem.Models.ExecutiveCommand;
using ExecutiveCommandStep = Kyoto.Bot.Core.ExecutiveCommandSystem.Models.ExecutiveCommandStep;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem;

public static class Converter
{
    public static ExecutiveCommand ToDomain(this ExecutiveCommandDal executiveCommand)
    {
        return ExecutiveCommand.Create(
            executiveCommand.SessionId,
            executiveCommand.ChatId,
            executiveCommand.ExternalUserId,
            executiveCommand.Command,
            executiveCommand.AdditionalData,
            (ExecutiveCommandStep)executiveCommand.Step,
            (CommandStepState)executiveCommand.StepState);
    }
}