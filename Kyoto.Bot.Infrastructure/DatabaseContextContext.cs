using Kyoto.Bot.Core.Database;
using Kyoto.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Infrastructure;

public class DatabaseContextContext : BaseDatabaseContextContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<ExternalUser>? TelegramUsers { get; set; }
    public DbSet<Models.Bot>? Bots { get; set; }
    public DbSet<MenuPanel>? MenuPanels { get; set; }

    public DatabaseContextContext(string connectionString) : base(connectionString)
    {
        
    }
}