using Microsoft.AspNetCore.Mvc;
using ReservationManagement.Api.Reservations.Contracts;

namespace ReservationManagement.Api.Reservations;

public static class WebApplicationExtensions
{
    public static WebApplication UseReservationsApi(this WebApplication app)
    {
        app.MapPost("api/reservations",
            async (ICommandHandler<CreateReservation> handler, CancellationToken cancellationToken) =>
            {
                var command = new CreateReservation();
                await handler.HandleAsync(command, cancellationToken);
                
                return Results.Created($"/api/v1/messages/{command.CreatedId}", null);
            });
        
        app.MapPost("api/reservations/{id}/resources",
            async ([FromRoute] Guid id, [FromBody] Guid resourceId, [FromHeader(Name = "If-Match")] long version, ICommandHandler<ReserveResource> handler, CancellationToken cancellationToken) =>
            {
                
                await handler.HandleAsync(new ReserveResource { ReservationId = id, ResourceId = resourceId, Version = version}, cancellationToken);
                return Results.NoContent();
            });
        
        app.MapPost("api/reservations/{id}/confirm",
            async ([FromRoute] Guid id, [FromHeader(Name = "If-Match")] long version, ICommandHandler<ConfirmReservation> handler, CancellationToken cancellationToken) =>
            {
                
                await handler.HandleAsync(new ConfirmReservation { Id = id, Version = version}, cancellationToken);
                return Results.NoContent();
            });
        
        app.MapGet("api/reservations/{id}",
            ([FromRoute] Guid id, IQueryHandler<GetReservation, GetReservation.ReservationDto> handler, CancellationToken cancellationToken) 
                => handler.HandleAsync(new GetReservation(id), cancellationToken));
        
        return app;
    }
}