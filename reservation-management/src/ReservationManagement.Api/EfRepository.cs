using Microsoft.EntityFrameworkCore;

namespace ReservationManagement.Api;

public class EfRepository<TEntity>(DbContext dbContext, EntityQueryInclude<TEntity>? queryInclude = null) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly Dictionary<Guid, long> _localEntityVersions = new();
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();

    private IQueryable<TEntity> Query => queryInclude is null ? _dbSet : queryInclude(_dbSet);
    
    public async Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = _dbSet.Local.SingleOrDefault(x => x.Id == id) ??
                     await Query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);

        if (entity is not null) 
            _localEntityVersions[entity.Id] = entity.Version;
        
        return entity;
    }

    public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken)
        => await FindAsync(id, cancellationToken) ?? throw EntityNotFoundException.Create<TEntity>(id);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(TEntity entity, long version, CancellationToken cancellationToken)
    {
        AssertConcurrencyToken(version, entity);
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(TEntity entity, long version, CancellationToken cancellationToken)
    {
        AssertConcurrencyToken(version, entity);
        _dbSet.Remove(entity);
        return dbContext.SaveChangesAsync(cancellationToken);
    }
    
    private void AssertConcurrencyToken(long originVersion, IEntity entity)
    {
        if (!_localEntityVersions.TryGetValue(entity.Id, out var readVersion))
            readVersion = dbContext.Entry(entity).Property(x => x.Version).OriginalValue;

        if (originVersion != readVersion)
            throw new InvalidOperationException($"Entity {entity.Id} has been modified during concurrency.");
    }
}

public delegate IQueryable<TEntity> EntityQueryInclude<TEntity>(IQueryable<TEntity> query)
    where TEntity : IEntity;