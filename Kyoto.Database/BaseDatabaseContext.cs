using Kyoto.Database.CommonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kyoto.Database;

public class BaseDatabaseContext : DbContext, IDatabaseContext
{
    private string _connectionString;
    
    public DbSet<Command>? Commands { get; set; }
    public DbSet<ExternalUser>? ExternalUsers { get; set; }
    public DbSet<TemplateMessage>? TemplateMessages { get; set; }
    public DbSet<TemplateMessageType>? TemplateMessageTypes { get; set; }
    public DbSet<MenuPanel>? MenuPanels { get; set; }
    public DbSet<MenuButton>? MenuButtons { get; set; }
    public DbSet<SystemStatus>? SystemStatuses { get; set; }

    public BaseDatabaseContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }

    public async Task MigrateAsync(string connectionString, CancellationToken cancellationToken = default) 
    {
        _connectionString = connectionString;
        await Database.MigrateAsync(cancellationToken);
    }

    public new EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : BaseModel
    {
        return base.Add(entity);
    }

    public new ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseModel
    {
        return base.AddAsync(entity, cancellationToken);
    }

    public async Task<int> SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseModel
    {
        await AddAsync(entity, cancellationToken);
        return await SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveRangeAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseModel
    {
        await AddRangeAsync(entity, cancellationToken);
        return await SaveChangesAsync(cancellationToken);
    }

    public Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : BaseModel
    {
        return base.AddRangeAsync(entities, cancellationToken);
    }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseModel
    {
        return base.Set<TEntity>();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : BaseModel
    {
        entity.ModificationTime = DateTime.UtcNow;
        base.Update(entity);
    }

    public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseModel
    {
        foreach (TEntity entity in entities)
        {
            Update(entity);
        }
    }

    public new void Remove<TEntity>(TEntity entity) where TEntity : BaseModel
    {
        base.Remove(entity);
    }

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseModel
    {
        base.RemoveRange(entities);
    }

    public void DetachAll()
    {
        ChangeTracker.Clear();
    }
}