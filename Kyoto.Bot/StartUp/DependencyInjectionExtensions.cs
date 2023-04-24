using Confluent.Kafka;
using Kyoto.Bot.Services.Authorization;
using Kyoto.Bot.Services.Command.CommandServices;
using Kyoto.Bot.Services.Command.ExecutiveCommandSystem;
using Kyoto.Bot.Services.Command.GlobalCommandServices;
using Kyoto.Bot.Services.Menu;
using Kyoto.Bot.Services.Processors;
using Kyoto.Bot.Services.RequestSender;
using Kyoto.Bot.StartUp.Settings;
using Kyoto.Domain.Authorization;
using Kyoto.Domain.BotUser;
using Kyoto.Domain.Command;
using Kyoto.Domain.Menu;
using Kyoto.Domain.PostSystem;
using Kyoto.Domain.Processors;
using Kyoto.Infrastructure;
using Kyoto.Infrastructure.Repositories.Authorization;
using Kyoto.Infrastructure.Repositories.BotUser;
using Kyoto.Infrastructure.Repositories.ExecutiveCommandSystem;
using Kyoto.Infrastructure.Repositories.Menu;
using Kyoto.Kafka;

namespace Kyoto.Bot.StartUp;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSettings(
        this IServiceCollection services,
        IConfiguration configuration,
        out AppSettings appSettings)
    {
        appSettings = new AppSettings();
        configuration.Bind(nameof(AppSettings), appSettings);
        return services.AddSingleton(appSettings);
    }
    
    public static IServiceCollection AddKafka(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = appSettings.KafkaBootstrapServers });
        services.AddKafkaConsumersFactory();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, DatabaseSettings databaseSettings)
    {
        return services.AddScoped<IDatabaseContext, DatabaseContext>(_ => new DatabaseContext(databaseSettings.ToConnectionString()));
    }
    
    public static IServiceCollection AddMenu(this IServiceCollection services)
    {
        return services
            .AddTransient<IMenuRepository, MenuRepository>()
            .AddTransient<IMenuService, MenuService>();
    }
    
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAuthorizationRepository, AuthorizationRepository>()
            .AddTransient<IAuthorizationService, AuthorizationService>();
    }

    public static IServiceCollection AddCommandServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IMessageCommandService, RegisterMessageCommandService>()
            .AddTransient<IExecutiveCommandFactory, ExecutiveCommandFactory>()
            .AddTransient<IExecutiveCommandRepository, ExecutiveCommandRepository>()
            .AddTransient<IExecutiveCommandService, ExecutiveCommandService>()
            .AddTransient<IStartCommandService, StartCommandService>();
    }

    public static IServiceCollection AddPostServices(this IServiceCollection services)
    {
        return services
            .AddTransient<AuthorizationPostService>()
            .AddTransient<MenuPostService>()
            .AddTransient<MenuPanelPostService>();
    }

    public static IServiceCollection AddOtherServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IPostService, PostService>()
            .AddTransient<IMessageService, MessageService>();
    }
}