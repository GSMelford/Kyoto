using Kyoto.Database.CommonModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Kyoto.Database;

public interface IDatabaseContext
{
    Task MigrateAsync(string connectionString, CancellationToken cancellationToken = default);
    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : BaseModel;
    ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseModel;
    Task<int> SaveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseModel;
    Task<int> SaveRangeAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseModel;
    Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : BaseModel;
    DbSet<TEntity> Set<TEntity>() where TEntity : BaseModel;
    void Update<TEntity>(TEntity entity) where TEntity : BaseModel;
    void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseModel;
    void Remove<TEntity>(TEntity entity) where TEntity : BaseModel;
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseModel;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    void DetachAll();
    Task DisposeAsync();
}