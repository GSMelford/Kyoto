using Kyoto.Domain.ExecutiveCommand.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ICommandStepFactory = Kyoto.Domain.ExecutiveCommand.Interfaces.ICommandStepFactory;

namespace Kyoto.Services.ExecuteCommand;

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