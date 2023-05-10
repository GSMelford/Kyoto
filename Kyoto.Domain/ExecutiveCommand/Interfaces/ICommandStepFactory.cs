namespace Kyoto.Domain.ExecutiveCommand.Interfaces;

public interface ICommandStepFactory
{
    string CommandName { get; }
    Type GetCommandStep(ExecutiveCommandStep commandStep);
    bool HasNext(ExecutiveCommandStep commandStep);
}