namespace Kyoto.Settings;

public class KyotoBotClientSettings
{
    public KafkaSettings KafkaSettings { get; set; } = null!;
    public DatabaseSettings DatabaseSettings { get; set; } = null!;
}