using MongoDB.Driver;

namespace tlou_infected_api.Data;

public class AppDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IConfiguration _configuration;

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        
        var connectionString = configuration.GetConnectionString("DbConnection");
        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
    }
    
    public IMongoDatabase? Database => _database;

    //public IMongoCollection<Person> People => _database.GetCollection<Person>("People");
}