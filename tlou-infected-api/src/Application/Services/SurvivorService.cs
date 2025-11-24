using System.Data;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Domain.Enums;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Application.Services;

public class SurvivorService(IMongoRepository<Survivor> survivorRepository)
{
    public async Task<Survivor> Create(SurvivorDto createSurvivorDto)
    {
        var survivor = createSurvivorDto.BuildSurvivor();
        
        await survivorRepository.AddAsync(survivor);
        return survivor;
    }

    public async Task<IEnumerable<Survivor>> GetAll()
    {
        return await survivorRepository.GetAllAsync();
    }
    
    public async Task<Survivor> GetSurvivorById(string id)
    {
        var survivor = await survivorRepository.GetByIdAsync(id);
        return survivor;
    }
    
    public async Task<string> GetSurvivorStatus(string id)
    {
        var survivor = await survivorRepository.GetByIdAsync(id);
        return SurvivorStatusEnum.GetName(typeof(SurvivorStatusEnum), survivor.Status);
    }
    
    public async Task<bool> UpdateSurvivor(SurvivorDto createSurvivorDto)
    {
        var survivorUpdate = createSurvivorDto.BuildSurvivor();
        await survivorRepository.UpdateAsync(createSurvivorDto.Id, survivorUpdate);
        return true;
    }
    
    public async Task<bool> UpdateLocation(string id, GeoJsonPoint<GeoJson2DGeographicCoordinates> location)
    {
        try
        {
            var survivor =  await survivorRepository.GetByIdAsync(id);
            survivor.Location = location;
            await survivorRepository.UpdateAsync(id, survivor);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> DeleteSurvivor(string id)
    {
        await survivorRepository.DeleteAsync(id);
        return true;
    }
}