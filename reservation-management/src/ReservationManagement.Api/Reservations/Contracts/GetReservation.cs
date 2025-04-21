namespace ReservationManagement.Api.Reservations.Contracts;

public class GetReservation(Guid id) : IQuery<GetReservation.ReservationDto>
{
    public Guid Id { get; } = id;
    
    public class ReservationDto
    {
        public required string Code { get; init; }
        public required IReadOnlyCollection<ReservationResourceDto> Resources { get; init; }
        public DateTime ExpiresAtUtc { get; init; }
        public bool IsConfirmed { get; init; }
        public DateTime CreatedAtUtc { get; init; }
        public DateTime? UpdatedAtUtc { get; init; }
        public long Version { get; init; }
    }
    
    public record ReservationResourceDto(Guid Id);
}