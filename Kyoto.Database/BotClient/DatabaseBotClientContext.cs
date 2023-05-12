using Kyoto.Database.BotClient.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Database.BotClient;

public class DatabaseBotClientContext : BaseDatabaseContext
{
    public DbSet<PostEvent>? EventMessages { get; set; }
    public DbSet<PreparedPost>? PreparedMessages { get; set; }
    
    public DatabaseBotClientContext(string connectionString = "Host=;Port=;Database=;Username=;Password=;") : base(connectionString)
    {
    }
}