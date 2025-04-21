using ReservationManagement.Api.Reservations.Contracts;

namespace ReservationManagement.Api.Reservations;

public class ReservationFactory(ICodeProvider codeProvider)
{
    public async Task<(Reservation Aggregate, ReservationCreated Event)> CreateAsync()
    {
        var code = await codeProvider.GetNextAsync();
        var createdAtUtc = DateTime.UtcNow;
        var expiresAtUtc = createdAtUtc.AddMinutes(4); // consider configuration
        
        var aggregate = new Reservation(id: Guid.NewGuid(),
            code: code,
            expiresAtUtc: expiresAtUtc,
            createdAtUtc: createdAtUtc);
        
        return (aggregate, aggregate.Created());
    }
}