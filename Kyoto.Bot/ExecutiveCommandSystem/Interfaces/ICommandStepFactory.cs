using ExecutiveCommandStep = Kyoto.Bot.Core.ExecutiveCommandSystem.Models.ExecutiveCommandStep;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces;

public interface ICommandStepFactory
{
    string CommandName { get; }
    Type GetCommandStep(ExecutiveCommandStep commandStep);
    bool HasNext(ExecutiveCommandStep commandStep);
}