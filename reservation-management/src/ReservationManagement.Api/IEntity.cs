namespace ReservationManagement.Api;

public interface IEntity
{
    Guid Id { get; }
    long Version { get; }
}