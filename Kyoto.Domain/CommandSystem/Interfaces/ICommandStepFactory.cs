namespace Kyoto.Domain.CommandSystem.Interfaces;

public interface ICommandStepFactory
{
    string CommandName { get; }
    Type GetCommandStep(int step);
    bool HasNext(int step);
}