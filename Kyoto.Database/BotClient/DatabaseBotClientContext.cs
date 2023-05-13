using Kyoto.Database.CommonModels;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Database.BotClient;

public class DatabaseBotClientContext : BaseDatabaseContext
{
    public DbSet<PostEvent>? EventMessages { get; set; }
    public DbSet<PreparedMessage>? PreparedMessages { get; set; }
    
    public DatabaseBotClientContext(string connectionString = "Host=;Port=;Database=;Username=;Password=;") : base(connectionString)
    {
    }
}