using MongoDB.Bson;
using MongoDB.Driver;

namespace tlou_infected_api.Repository;

public class MongoRepository<T>(IMongoDatabase database) : IMongoRepository<T>
    where T : class
{
    private readonly IMongoCollection<T> _collection = database.GetCollection<T>(GetCollectionName());

    private static string GetCollectionName()
    {
        // Convenção: nome da classe + "s" (ex: Survivor -> Survivors)
        return typeof(T).Name + "s";
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("_id", new ObjectId(id))).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(string id, T entity)
    {
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)), entity);
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)));
    }
}