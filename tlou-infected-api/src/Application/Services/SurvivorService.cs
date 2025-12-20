using MongoDB.Driver;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Domain.Enums;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Application.Services;

public class SurvivorService(IMongoRepository<Survivor> survivorRepository,
    IMongoRepository<InventorySurvivor> inventoryRepository)
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
    
    public async Task<IEnumerable<InventorySurvivor>> GetSurvivorInventory()
    {
        var survivorInventory = await inventoryRepository.GetAllAsync();
        return await inventoryRepository.GetAllAsync();
    }
    
    public async Task<bool> UpdateSurvivor(SurvivorDto createSurvivorDto)
    {
        var survivorUpdate = createSurvivorDto.BuildSurvivor();
        await survivorRepository.UpdateAsync(createSurvivorDto.Id, survivorUpdate);
        return true;
    }

    public async Task<bool> DeleteSurvivor(string id)
    {
        await survivorRepository.DeleteAsync(id);
        return true;
    }
}