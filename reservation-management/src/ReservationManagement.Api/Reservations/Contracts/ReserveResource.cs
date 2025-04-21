namespace ReservationManagement.Api.Reservations.Contracts;

public class ReserveResource : ICommand
{
    public Guid ReservationId { get; init; }
    public Guid ResourceId { get; init; }
    public long Version { get; init; }
}