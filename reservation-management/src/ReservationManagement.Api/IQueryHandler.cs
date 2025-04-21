namespace ReservationManagement.Api;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
{
    Task<TResult> HandleAsync(TQuery command, CancellationToken cancellationToken);
}

public interface IQuery<TResult>;