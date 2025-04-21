using ReservationManagement.Api.Reservations.Contracts;

namespace ReservationManagement.Api.Reservations;

public class ConfirmReservationHandler(IRepository<Reservation> repository) : ICommandHandler<ConfirmReservation>
{
    public async Task HandleAsync(ConfirmReservation command, CancellationToken cancellationToken)
    {
        var aggregate = await repository.GetAsync(command.Id, cancellationToken);
        aggregate.Confirm();
        await repository.UpdateAsync(aggregate, command.Version, cancellationToken);
    }
}