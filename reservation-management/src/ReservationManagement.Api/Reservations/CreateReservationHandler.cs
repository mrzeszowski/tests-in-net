using ReservationManagement.Api.Reservations.Contracts;

namespace ReservationManagement.Api.Reservations;

public class CreateReservationHandler(IRepository<Reservation> repository, ReservationFactory factory) : ICommandHandler<CreateReservation>
{
    public async Task HandleAsync(CreateReservation command, CancellationToken cancellationToken)
    {
        var (aggregate, @event) = await factory.CreateAsync();
        command.CreatedId = aggregate.Id;
        await repository.AddAsync(aggregate, cancellationToken);
    }
}