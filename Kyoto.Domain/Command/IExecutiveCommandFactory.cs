namespace Kyoto.Domain.Command;

public interface IExecutiveCommandFactory
{
    ICommandStepFactory GetCommandStepFactory(CommandType commandType);
}