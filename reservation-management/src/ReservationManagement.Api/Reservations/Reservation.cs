using ReservationManagement.Api.Reservations.Contracts;

namespace ReservationManagement.Api.Reservations;

public class Reservation : IEntity
{
    public Guid Id { get; }
    public string Code { get; }

    private readonly List<ReservationResource> _resources = new();
    public IReadOnlyCollection<ReservationResource> Resources => _resources;

    public DateTime ExpiresAtUtc { get; }
    public bool IsConfirmed { get; private set; }
    public DateTime CreatedAtUtc { get; }
    public DateTime? UpdatedAtUtc { get; private set; }
    
    public long Version { get; private set; } = 1;

    private Reservation() { }

    public Reservation(Guid id,
        string code,
        DateTime expiresAtUtc,
        DateTime createdAtUtc)
    {
        Id = id;
        Code = code;
        ExpiresAtUtc = expiresAtUtc;
        CreatedAtUtc = createdAtUtc;
    }

    public ReservationCreated Created()
    {
        return new ReservationCreated
        {
            Id = Id,
            Data = new ReservationCreated.ReservationDto
            {
                Code = Code,
                Resources = Resources.Select(x => new ReservationCreated.ReservationResourceDto(x.ResourceId)).ToArray(),
                ExpiresAtUtc = ExpiresAtUtc,
                CreatedAtUtc = CreatedAtUtc
            },
            Version = Version
        };
    }

    public ResourceReserved ReserveResource(ReserveResource command)
    {
        AssertCanChange();
        if (Resources.Any(x => x.ResourceId == command.ResourceId))
            throw new InvalidOperationException($"Resource with id {command.ResourceId} already reserved.");
        
        _resources.Add(new ReservationResource(command.ResourceId, Id));
        UpdatedAtUtc = DateTime.UtcNow;
        Version++;

        return new ResourceReserved
        {
            ReservationId = Id,
            ResourceId = command.ResourceId,
            Version = Version
        };
    }

    public ReservationConfirmed Confirm()
    {
        AssertCanChange();
        
        IsConfirmed = true;
        UpdatedAtUtc = DateTime.UtcNow;
        Version++;

        return new ReservationConfirmed
        {
            Id = Id,
            Version = Version
        };
    }

    private void AssertCanChange()
    {
        if (IsConfirmed || ExpiresAtUtc < DateTime.UtcNow)
        {
            throw new InvalidOperationException("Cannot change reservation");
        }
    }
}

public class ReservationResource
{
    public Guid ResourceId { get; private set; }
    public Guid ReservationId { get; private set; }

    public ReservationResource() { }

    public ReservationResource(Guid resourceId, Guid reservationId)
    {
        ResourceId = resourceId;
        ReservationId = reservationId;
    }
}