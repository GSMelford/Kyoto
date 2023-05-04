namespace Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces;

public interface IExecutiveCommandFactory
{
    ICommandStepFactory GetCommandStepFactory(string commandName);
}