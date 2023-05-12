namespace Kyoto.Domain.CommandSystem.Interfaces;

public interface ICommandFactory
{
    ICommandStepFactory GetCommandStepFactory(string commandName);
}