namespace Kyoto.Domain.Command;

public interface ICommandStepFactory
{
    CommandType CommandType { get; }
    Type GetCommandStep(ExecutiveCommandStep commandStep);
    bool HasNext(ExecutiveCommandStep commandStep);
}