// csharp
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace tlou_infected_api.Data;

public class AppDbContext
{
    public IMongoDatabase? Database { get; }

    public AppDbContext(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDB:ConnectionString"];
        var databaseName = configuration["MongoDB:Database"];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("MongoDB:ConnectionString não configurado.");
        if (string.IsNullOrWhiteSpace(databaseName))
            throw new InvalidOperationException("MongoDB:Database não configurado.");

        var client = new MongoClient(connectionString);
        Database = client.GetDatabase(databaseName);
    }
}