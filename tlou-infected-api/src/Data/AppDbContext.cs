using MongoDB.Driver;

namespace tlou_infected_api.Data;

public class AppDbContext
{
    private readonly IMongoDatabase _database;

    public AppDbContext()
    {
        var client = new MongoClient("");
        _database = client.GetDatabase("tlou-api");
    }

    //public IMongoCollection<Person> People => _database.GetCollection<Person>("People");
}