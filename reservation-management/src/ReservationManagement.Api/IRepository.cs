namespace ReservationManagement.Api;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken);
    Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, long version, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, long version, CancellationToken cancellationToken);
}