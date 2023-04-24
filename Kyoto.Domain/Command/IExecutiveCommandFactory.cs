namespace Kyoto.Domain.Command;

public interface IExecutiveCommandFactory
{
    IMessageCommandService CreateMessageCommandService(ExecutiveCommandType commandType);
    ICallbackQueryCommandService CreateCallbackQueryCommandService(ExecutiveCommandType commandType);
}