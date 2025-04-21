using ReservationManagement.Api.Reservations.Contracts;

namespace ReservationManagement.Api.Reservations;

public class GetReservationHandler(IRepository<Reservation> repository) : IQueryHandler<GetReservation, GetReservation.ReservationDto>
{
    public async Task<GetReservation.ReservationDto> HandleAsync(GetReservation command, CancellationToken cancellationToken)
    {
        var aggregate = await repository.GetAsync(command.Id, cancellationToken);
        return new GetReservation.ReservationDto
        {
            Code = aggregate.Code,
            CreatedAtUtc = aggregate.CreatedAtUtc,
            ExpiresAtUtc = aggregate.ExpiresAtUtc,
            IsConfirmed = aggregate.IsConfirmed,
            Resources = aggregate.Resources.Select(x => new GetReservation.ReservationResourceDto(x.ResourceId)).ToArray(),
            UpdatedAtUtc = aggregate.UpdatedAtUtc,
            Version = aggregate.Version
        };
    }
}