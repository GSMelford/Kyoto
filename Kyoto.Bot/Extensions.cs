using Kyoto.Bot.Core.ExecutiveCommandSystem;
using Kyoto.Bot.Core.ExecutiveCommandSystem.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.Bot.Core;

public static class Extensions
{
    public static IServiceCollection AddExecutiveCommandSystem(this IServiceCollection services)
    {
        return services
            .AddTransient<IExecutiveCommandFactory, ExecutiveCommandFactory>()
            .AddTransient<IExecutiveCommandRepository, ExecutiveCommandRepository>()
            .AddTransient<IExecutiveCommandService, BaseExecutiveCommandService>();
    }
}