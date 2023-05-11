using Kyoto.Commands.BotFactory.BotRegistrationCommand;
using Kyoto.Commands.BotFactory.DeployBotCommand;
using Kyoto.Commands.BotFactory.GlobalCommands;
using Kyoto.Commands.BotFactory.RegistrationCommand;
using Kyoto.Dal.CommonRepositories.ExecuteCommandSystem;
using Kyoto.Domain.BotFactory.GlobalCommand.Interfaces;
using Kyoto.Domain.ExecutiveCommand.Interfaces;
using Kyoto.Services.BotFactory.ExecutiveCommand;
using Kyoto.Services.ExecuteCommand;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.DI;

public static class CommandExtensions
{
    public static IServiceCollection AddFactoryCommands(this IServiceCollection services)
    {
        return services
            .AddTransient<IExecutiveCommandService, FactoryExecutiveCommandService>()
            .AddTransient<ICommandStepFactory, RegistrationCommandStepFactory>()
            .AddTransient<ICommandStepFactory, BotRegistrationCommandStepFactory>()
            .AddTransient<ICommandStepFactory, DeployBotCommandStepFactory>()
            .AddTransient<IStartCommandService, StartCommandService>();
    }
    
    public static IServiceCollection AddGlobalCommands(this IServiceCollection services)
    {
        return services.AddTransient<IStartCommandService, StartCommandService>();
    }
    
    public static IServiceCollection AddExecutiveCommand(this IServiceCollection services)
    {
        return services
            .AddTransient<IExecutiveCommandFactory, ExecutiveCommandFactory>()
            .AddTransient<IExecutiveCommandRepository, ExecutiveCommandRepository>();
    }
}