namespace Kyoto.Bot.StartUp.Settings;

public class AppSettings
{
    public string KafkaBootstrapServers { get; set; } = null!;
    public DatabaseSettings DatabaseSettings { get; set; } = null!;
}