namespace ReservationManagement.Api;

public class EntityNotFoundException(string entityName, Guid entityId)
    : Exception($"{entityName} with id: {entityId} does not exist.")
{
    public string EntityName { get; } = entityName;
    public Guid EntityId { get; } = entityId;

    public static EntityNotFoundException Create<TEntity>(Guid entityId) => new(typeof(TEntity).Name, entityId);
}