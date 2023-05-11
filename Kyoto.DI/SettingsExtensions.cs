using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.DI;

public static class SettingsExtensions
{
    public static IServiceCollection AddSettings<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        out T settings) where T : class, new()
    {
        settings = new T();
        configuration.Bind(typeof(T).Name, settings);
        return services.AddSingleton(settings);
    }
}