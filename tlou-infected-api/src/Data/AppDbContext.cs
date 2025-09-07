using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace tlou_infected_api.Data;

public class AppDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        
        var connectionString = configuration["MONGODB_URI"];
        var databaseName = configuration["MONGODB_DATABASE"];
        
        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        
        _database = mongoClient.GetDatabase(databaseName ?? mongoUrl.DatabaseName);
    }
    
    public IMongoDatabase Database => _database;
}