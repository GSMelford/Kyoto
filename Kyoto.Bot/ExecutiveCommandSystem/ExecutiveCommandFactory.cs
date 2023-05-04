using Microsoft.Extensions.DependencyInjection;
using ICommandStepFactory = Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces.ICommandStepFactory;
using IExecutiveCommandFactory = Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces.IExecutiveCommandFactory;

namespace Kyoto.Bot.Core.ExecutiveCommandSystem;

public class ExecutiveCommandFactory : IExecutiveCommandFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ExecutiveCommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandStepFactory GetCommandStepFactory(string commandName)
    {
        var services = _serviceProvider.GetServices<ICommandStepFactory>();
        return services.First(x => x.CommandName == commandName);
    }
}