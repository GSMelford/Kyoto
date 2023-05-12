using Kyoto.Domain.CommandSystem.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ICommandStepFactory = Kyoto.Domain.CommandSystem.Interfaces.ICommandStepFactory;

namespace Kyoto.Services.CommandSystem;

public class CommandFactory : ICommandFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandStepFactory GetCommandStepFactory(string commandName)
    {
        var services = _serviceProvider.GetServices<ICommandStepFactory>();
        return services.First(x => x.CommandName == commandName);
    }
}