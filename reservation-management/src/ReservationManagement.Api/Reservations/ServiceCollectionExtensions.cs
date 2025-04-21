using ReservationManagement.Api.Reservations.Contracts;

namespace ReservationManagement.Api.Reservations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReservations(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateReservation>, CreateReservationHandler>();
        services.AddScoped<ICommandHandler<ReserveResource>, ReserveResourceHandler>();
        services.AddScoped<ICommandHandler<ConfirmReservation>, ConfirmReservationHandler>();
        services.AddScoped<IQueryHandler<GetReservation, GetReservation.ReservationDto>, GetReservationHandler>();

        services.AddScoped<ReservationFactory>();
        services.AddScoped<EntityQueryInclude<Reservation>>(_ => ReservationEfConfiguration.Include);
        services.AddScoped<IRepository<Reservation>, EfRepository<Reservation>>();
        
        return services;
    }
}