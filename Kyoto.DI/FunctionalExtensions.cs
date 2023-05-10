using Kyoto.Dal.BotFactory.Repositories.Bot;
using Kyoto.Dal.BotFactory.Repositories.BotUser;
using Kyoto.Dal.BotFactory.Repositories.Menu;
using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.BotFactory.Menu.Interfaces;
using Kyoto.Domain.BotFactory.User.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Services.BotFactory.Bot;
using Kyoto.Services.BotFactory.Menu;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.BotFactory.Processors;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.DI;

public static class FunctionalExtensions
{
    public static IServiceCollection AddMenu(this IServiceCollection services)
    {
        return services
            .AddTransient<IMenuRepository, MenuRepository>()
            .AddTransient<IMenuService, MenuService>();
    }
    
    public static IServiceCollection AddPostServices(this IServiceCollection services)
    {
        return services.AddTransient<MenuPanelPostService>();
    }
    
    public static IServiceCollection AddBot(this IServiceCollection services)
    {
        return services
            .AddTransient<IBotRepository, BotRepository>()
            .AddTransient<IBotService, BotService>();
    }
    
    public static IServiceCollection AddProcessorServices(this IServiceCollection services)
    {
        return services
            .AddTransient<ICallbackQueryService, CallbackQueryService>()
            .AddTransient<IMessageService, MessageService>();
    }

    public static IServiceCollection AddPostService(this IServiceCollection services)
    {
        return services.AddTransient<IPostService, PostService>();
    }

    public static IServiceCollection AddUser(this IServiceCollection services)
    {
        return services.AddTransient<IUserRepository, UserRepository>();
    }
}