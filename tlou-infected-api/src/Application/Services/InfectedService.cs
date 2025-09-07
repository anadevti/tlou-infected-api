using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Application.Services;

public class InfectedService
{
    
    private readonly IMongoCollection<Infected>? _infectedCollection;

    public InfectedService(AppDbContext appDbContext )
    {
        _infectedCollection = appDbContext.Database?.GetCollection<Infected>("infected");
    }

    public async Task<Infected> CreateInfected(CreateInfectedDto createInfectedDto)
    {
        // method post
        var infected = new Infected
        {
            Type = InfectedStageSmartEnum.FromName(createInfectedDto.Type),
            Description = createInfectedDto.Description,
            Image = createInfectedDto.Image,
            Weaknesses = createInfectedDto.Weaknesses
        };
        await _infectedCollection.InsertOneAsync(infected);
        return infected;
    }
    public async Task<bool> UpdateInfected(Infected infected)
    {
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
}