using Kyoto.Database.BotFactory.Repositories.Bot;
using Kyoto.Database.BotFactory.Repositories.BotUser;
using Kyoto.Database.CommonRepositories.Menu;
using Kyoto.Database.CommonRepositories.PreparedMessage;
using Kyoto.Database.CommonRepositories.TemplateMessage;
using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.BotFactory.User.Interfaces;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.PreparedMessagesSystem;
using Kyoto.Domain.Processors.Interfeces;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Services.BotClient.TemplateMessage;
using Kyoto.Services.BotFactory.Bot;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.Menu;
using Kyoto.Services.PreparedMessagesSystem;
using Kyoto.Services.Processors;
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

    public static IServiceCollection AddTemplateMessage(this IServiceCollection services)
    {
        return services
            .AddTransient<ITemplateMessageService, TemplateMessageService>()
            .AddTransient<ITemplateMessageRepository, TemplateMessageRepository>();
    }
    
    public static IServiceCollection AddPreparedMessages(this IServiceCollection services)
    {
        return services
            .AddTransient<IPreparedMessagesRepository, PreparedMessagesRepository>()
            .AddTransient<IPreparedMessagesService, PreparedMessagesService>();
    }
}