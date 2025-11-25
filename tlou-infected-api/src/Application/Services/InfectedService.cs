using MongoDB.Driver;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;

namespace tlou_infected_api.Application.Services;

public class InfectedService
{
    
    private readonly IMongoCollection<Infected>? _infectedCollection;

    public InfectedService(AppDbContext appDbContext )
    {
        _infectedCollection = appDbContext.Database?.GetCollection<Infected>("infected");
    }
    
    public async Task<Infected> CreateInfected(InfectedDto createInfectedDto)
    {
        var infected = createInfectedDto.BuildInfected();
        var isValid = ValidatedInfected(infected, i => !string.IsNullOrWhiteSpace(i.Image));

        if (!isValid)
        {
            throw new ApplicationException("Invalid infected data");
        }
            
        await _infectedCollection.InsertOneAsync(infected);
        return infected;
    }
    
    public async Task<bool> GetInfectedById(string id)
    {
        var filter = Builders<Infected>.Filter.Eq(x => x.Id, id);
        var infected = await _infectedCollection.Find(filter).FirstOrDefaultAsync();
        return infected != null;
    }
    
    public async Task<bool> UpdateInfected(InfectedDto createInfectedDto)
    {
        var infected = createInfectedDto.BuildInfected();
        var isValid = ValidatedInfected(infected, i => i.Image.EndsWith(".png", StringComparison.OrdinalIgnoreCase));

        if (!isValid)
        {
            throw new Exception($"Invalid image format");
        }
        
        var filter = Builders<Infected>.Filter.Eq(f => f.Id, infected.Id);
        var result = await _infectedCollection.ReplaceOneAsync(filter, infected);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteInfected(string id)
    {
        var filter = Builders<Infected>.Filter.Eq(f => f.Id, id);
        await _infectedCollection.DeleteOneAsync(filter);
        return true;
    }

    private bool ValidatedInfected(Infected infected, Predicate<Infected> predicate)
    {
        bool isValid = predicate(infected);
        return isValid;
    }
    
}