namespace ReservationManagement.Api.Reservations.Contracts;

public class ResourceReserved
{
    public Guid ReservationId { get; set; }
    public Guid ResourceId { get; set; }
    public long Version { get; set; }
}