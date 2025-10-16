using MongoDB.Driver;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Application.Services;

public class SurvivorService
{
    private readonly IMongoCollection<Survivor>? _survivorCollection;
    
    public SurvivorService(AppDbContext appDbContext )
    {
        _survivorCollection = appDbContext.Database?.GetCollection<Survivor>("survivor");
    }

    public async Task<Survivor> Create(SurvivorDto createSurvivorDto)
    {
        var survivor = createSurvivorDto.BuildSurvivor();
        
        await _survivorCollection.InsertOneAsync(survivor);
        return survivor;
    }
    
    public async Task<bool> GetSurvivorById(string id)
    {
        var filter = Builders<Survivor>.Filter.Eq(x => x.Id, id);
        var survivor = await _survivorCollection.Find(filter).FirstOrDefaultAsync();
        return survivor != null;
    }
    
    public async Task<bool> UpdateSurvivor(SurvivorDto createSurvivorDto)
    {
        var survivorUpdate = createSurvivorDto.BuildSurvivor();
        var filter = Builders<Survivor>.Filter.Eq(f => f.Id, survivorUpdate.Id);
        var result = await _survivorCollection.ReplaceOneAsync(filter, survivorUpdate);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteSurvivor(string id)
    {
        var filter = Builders<Survivor>.Filter.Eq(x => x.Id, id);
        await _survivorCollection.DeleteManyAsync(filter);
        return true;
    }
}