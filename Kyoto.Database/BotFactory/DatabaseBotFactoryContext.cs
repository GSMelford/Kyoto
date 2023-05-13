using Kyoto.Database.BotFactory.Models;
using Kyoto.Database.CommonModels;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Database.BotFactory;

public class DatabaseBotFactoryContext : BaseDatabaseContext
{
    public DbSet<Bot>? Bots { get; set; }

    public DatabaseBotFactoryContext(string connectionString = "Host=;Port=;Database=;Username=;Password=;") : base(connectionString)
    {
        
    }
}