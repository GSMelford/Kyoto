namespace Kyoto.Domain.Settings;

public class AppSettings
{
    public string BaseUrl { get; set; } = null!;
    public string ReceiverEndpoint { get; set; } = null!;
    public KafkaSettings KafkaSettings { get; set; } = null!;
    public BotTenantSettings BotTenantSettings { get; set; } = null!;
    public DatabaseSettings DatabaseSettings { get; set; } = null!;
}