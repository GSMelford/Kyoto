namespace Kyoto.Database.BotClient;

public class DatabaseBotClientContext : BaseDatabaseContext
{
    public DatabaseBotClientContext(string connectionString = "Host=;Port=;Database=;Username=;Password=;") : base(connectionString)
    {
    }
}