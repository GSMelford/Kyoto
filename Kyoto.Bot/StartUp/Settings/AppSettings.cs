namespace Kyoto.Bot.StartUp.Settings;

public class AppSettings
{
    public string BaseUrl { get; set; } = null!;
    public string ReceiverEndpoint { get; set; } = null!;
    public string KafkaBootstrapServers { get; set; } = null!;
    public DatabaseSettings DatabaseSettings { get; set; } = null!;
}