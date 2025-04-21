namespace ReservationManagement.Api.Reservations.Contracts;

public class ConfirmReservation : ICommand
{
    public Guid Id { get; init; }
    public long Version { get; init; }
}