namespace Kyoto.Domain.ExecutiveCommand.Interfaces;

public interface IExecutiveCommandFactory
{
    ICommandStepFactory GetCommandStepFactory(string commandName);
}