using Kyoto.Commands.BotClient;
using Kyoto.Commands.BotFactory;
using Kyoto.Commands.BotFactory.AddNewsletterCommand;
using Kyoto.Commands.BotFactory.BotRegistrationCommand;
using Kyoto.Commands.BotFactory.DeployBotCommand;
using Kyoto.Commands.BotFactory.DisableBotCommand;
using Kyoto.Commands.BotFactory.SetRegistrationCommand;
using Kyoto.Commands.CommonCommnad.RegistrationCommand;
using Kyoto.Database.CommonRepositories.CommandSystem;
using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Services.CommandSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.DI;

public static class CommandExtensions
{
    public static IServiceCollection AddFactoryCommands(this IServiceCollection services)
    {
        return services
            .AddTransient<ICommandSet, BotFactoryCommandSet>()
            .AddTransient<ICommandStepFactory, RegistrationCommandStepFactory>()
            .AddTransient<ICommandStepFactory, BotRegistrationCommandStepFactory>()
            .AddTransient<ICommandStepFactory, DeployBotCommandStepFactory>()
            .AddTransient<ICommandStepFactory, DisableBotCommandStepFactory>()
            .AddTransient<ICommandStepFactory, SetRegistrationCommandStepFactory>()
            .AddTransient<ICommandStepFactory, AddNewsletterCommandStepFactory>();
    }
    
    public static IServiceCollection AddClientCommands(this IServiceCollection services)
    {
        return services
            .AddTransient<ICommandSet, BotClientCommandSet>()
            .AddTransient<ICommandStepFactory, RegistrationCommandStepFactory>();
    }

    public static IServiceCollection AddCommandSystem(this IServiceCollection services)
    {
        return services
            .AddTransient<ICommandFactory, CommandFactory>()
            .AddTransient<ICommandService, CommandService>()
            .AddTransient<ICommandRepository, CommandRepository>();
    }
}