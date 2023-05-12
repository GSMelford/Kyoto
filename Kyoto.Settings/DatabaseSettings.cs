namespace Kyoto.Settings;

public class DatabaseSettings
{
    public string Host { get; set; } = null!;
    public string Port { get; set; } = null!;
    public string Database { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string ToConnectionString(string? tenantKey = null)
    {
        var database = string.IsNullOrEmpty(tenantKey) ? Database : $"{tenantKey}.{Database}";
        return $"Host={Host};Port={Port};Database={database};Username={Username};Password={Password};";
    }
}