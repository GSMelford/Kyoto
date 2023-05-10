using Kyoto.Dal.BotFactory.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Dal.BotFactory;

public class DatabaseBotFactoryContext : BaseDatabaseContextContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<ExternalUser>? TelegramUsers { get; set; }
    public DbSet<Bot>? Bots { get; set; }
    public DbSet<MenuPanel>? MenuPanels { get; set; }

    public DatabaseBotFactoryContext(string connectionString = "Host=;Port=;Database=;Username=;Password=;") : base(connectionString)
    {
        
    }
}