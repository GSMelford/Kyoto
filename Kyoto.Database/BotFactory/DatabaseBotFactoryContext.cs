using Kyoto.Database.BotFactory.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Database.BotFactory;

public class DatabaseBotFactoryContext : BaseDatabaseContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<ExternalUser>? ExternalUsers { get; set; }
    public DbSet<Bot>? Bots { get; set; }
    public DbSet<MenuPanel>? MenuPanels { get; set; }

    public DatabaseBotFactoryContext(string connectionString = "Host=;Port=;Database=;Username=;Password=;") : base(connectionString)
    {
        
    }
}