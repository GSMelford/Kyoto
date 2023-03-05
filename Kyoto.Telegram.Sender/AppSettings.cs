namespace Kyoto.Telegram.Sender;

public class AppSettings
{
    public string TelegramBotToken { get; set; } = null!;
    public string KafkaBootstrapServers { get; set; } = null!;
}