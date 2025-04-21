namespace ReservationManagement.Api.Reservations.Contracts;

public class CreateReservation : ICommand
{
    public Guid CreatedId { get; set; }
}