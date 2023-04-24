using Kyoto.Bot.Services.Command.CommandServices;
using Kyoto.Domain.Command;

namespace Kyoto.Bot.Services.Command.ExecutiveCommandSystem;

public class ExecutiveCommandFactory : IExecutiveCommandFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ExecutiveCommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMessageCommandService CreateMessageCommandService(ExecutiveCommandType commandType)
    {
        var services = _serviceProvider.GetServices<IMessageCommandService>();
        return services.First(x => x.ExecutiveCommandType == commandType);
    }
    
    public ICallbackQueryCommandService CreateCallbackQueryCommandService(ExecutiveCommandType commandType)
    {
        var services = _serviceProvider.GetServices<ICallbackQueryCommandService>();
        return services.First(x => x.ExecutiveCommandType == commandType);
    }
}