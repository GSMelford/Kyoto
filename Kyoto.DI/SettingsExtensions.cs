using Kyoto.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kyoto.DI;

public static class SettingsExtensions
{
    public static IServiceCollection AddSettings(
        this IServiceCollection services,
        IConfiguration configuration,
        out AppSettings appSettings)
    {
        appSettings = new AppSettings();
        configuration.Bind(nameof(AppSettings), appSettings);
        return services
            .AddSingleton(appSettings)
            .AddSingleton(appSettings.KafkaSettings);
    }
}