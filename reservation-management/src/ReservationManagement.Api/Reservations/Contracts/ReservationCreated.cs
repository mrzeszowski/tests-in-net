namespace ReservationManagement.Api.Reservations.Contracts;

public class ReservationCreated : IEvent
{
    public Guid Id { get; init; }
    public required ReservationDto Data { get; init; }
    public long Version { get; init; }

    public class ReservationDto
    {
        public required string Code { get; init; }
        public required IReadOnlyCollection<ReservationResourceDto> Resources { get; init; }
        public DateTime ExpiresAtUtc { get; init; }
        public DateTime CreatedAtUtc { get; init; }
    }

    public record ReservationResourceDto(Guid Id);
}
