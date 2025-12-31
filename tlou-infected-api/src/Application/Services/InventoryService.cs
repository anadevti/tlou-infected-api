using tlou_infected_api.Domain.DTO.Inventory;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Application.Services;


public class InventoryService (IMongoRepository<InventorySurvivor> inventoryRepository,
    IInventoryRepository _inventoryRepositoryUpsert)
{
    public async Task<InventorySurvivor> CreateInventory(CreateInventorySurvivorDto createInventorySurvivorDto)
    {
        var survivorInventory = createInventorySurvivorDto.BuildInventorySurvivor();
        
        await inventoryRepository.AddAsync(survivorInventory);
        return survivorInventory;
    }
    
    public async Task<IEnumerable<InventorySurvivor>> GetSurvivorInventory()
    {
        var survivorInventory = await inventoryRepository.GetAllAsync();
        return await inventoryRepository.GetAllAsync();
    }
    
    public async Task<IEnumerable<InventorySurvivor>> UpdateInventorySurvivors(InventorySurvivor inventory)
    {
        await _inventoryRepositoryUpsert.UpsertInventory(inventory);
        return new[] { inventory };
    }
    
}