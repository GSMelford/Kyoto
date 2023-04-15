using Confluent.Kafka;
using Kyoto.Bot.StartUp.Settings;
using Kyoto.Kafka;

namespace Kyoto.Bot.StartUp;

public static class DependencyInjectionExtensions
{
    public static void AddSettings(
        this IServiceCollection services,
        IConfiguration configuration,
        out AppSettings appSettings)
    {
        appSettings = new AppSettings();
        configuration.Bind(nameof(AppSettings), appSettings);
        services.AddSingleton(appSettings);
    }
    
    public static IServiceCollection AddKafka(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = appSettings.KafkaBootstrapServers });
        services.AddKafkaConsumersFactory();

        return services;
    } 
}