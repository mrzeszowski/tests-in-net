namespace ReservationManagement.Api;

public interface ICodeProvider
{
    Task<string> GetNextAsync();
}

public class GuidCodeProvider : ICodeProvider
{
    public Task<string> GetNextAsync() => Task.FromResult(Guid.NewGuid().ToString("N"));
}