namespace ReservationManagement.Api.Reservations.Contracts;

public class ReservationConfirmed : IEvent
{
    public Guid Id { get; init; }
    public long Version { get; init; }
}