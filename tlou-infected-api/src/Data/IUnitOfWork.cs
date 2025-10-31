using MongoDB.Driver;

namespace tlou_infected_api.Data;

public interface IUnitOfWork : IDisposable
{
    Task StartTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    IClientSessionHandle Session { get; }
}