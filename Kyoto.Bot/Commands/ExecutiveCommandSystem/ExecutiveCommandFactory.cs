using Kyoto.Domain.Command;

namespace Kyoto.Bot.Commands.ExecutiveCommandSystem;

public class ExecutiveCommandFactory : IExecutiveCommandFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ExecutiveCommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandStepFactory GetCommandStepFactory(CommandType commandType)
    {
        var services = _serviceProvider.GetServices<ICommandStepFactory>();
        return services.First(x => x.CommandType == commandType);
    }
}