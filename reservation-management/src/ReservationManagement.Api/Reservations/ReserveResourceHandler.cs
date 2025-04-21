using ReservationManagement.Api.Reservations.Contracts;

namespace ReservationManagement.Api.Reservations;

public class ReserveResourceHandler(IRepository<Reservation> repository) : ICommandHandler<ReserveResource>
{
    public async Task HandleAsync(ReserveResource command, CancellationToken cancellationToken)
    {
        var aggregate = await repository.GetAsync(command.ReservationId, cancellationToken);
        aggregate.ReserveResource(command);
        await repository.UpdateAsync(aggregate, command.Version, cancellationToken);
    }
}