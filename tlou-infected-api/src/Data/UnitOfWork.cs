using MongoDB.Driver;

namespace tlou_infected_api.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly IMongoClient _client;
    private IClientSessionHandle _session;

    public IClientSessionHandle Session => _session;

    public UnitOfWork(IMongoClient client)
    {
        _client = client;
    }

    public async Task StartTransactionAsync()
    {
        _session = await _client.StartSessionAsync();
        _session.StartTransaction();
    }

    public async Task CommitAsync()
    {
        await _session.CommitTransactionAsync();
    }

    public async Task RollbackAsync()
    {
        await _session.AbortTransactionAsync();
    }

    public void Dispose()
    {
        _session?.Dispose();
    }
}