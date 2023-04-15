using Confluent.Kafka;
using Kyoto.Bot.StartUp.Settings;
using Kyoto.Infrastructure;
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
        return services.AddScoped<IDatabaseContext>(_ => new DatabaseContext(databaseSettings.ToConnectionString()));
    }
}