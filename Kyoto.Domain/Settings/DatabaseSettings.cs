namespace Kyoto.Domain.Settings;

public class DatabaseSettings
{
    public string Host { get; set; } = null!;
    public string Port { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string ToConnectionString()
    {
        return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password};";
    }
}