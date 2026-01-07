using tlou_infected_api.Domain.DTO.Inventory;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Application.Services;


public class InventoryService (IInventoryRepository inventoryRepository)
{
    public async Task<InventorySurvivor> CreateInventory(CreateInventorySurvivorDto createInventorySurvivorDto)
    {
        var survivorInventory = createInventorySurvivorDto.BuildInventorySurvivor();
        
        await inventoryRepository.AddAsync(survivorInventory);
        return survivorInventory;
    }
    
    public async Task<IEnumerable<InventorySurvivor>> GetSurvivorInventory()
    {
        return await inventoryRepository.GetAllAsync();
    }
    
    public async Task<IEnumerable<InventorySurvivor>> UpdateInventorySurvivors(InventorySurvivor inventory)
    {
        await inventoryRepository.UpsertInventory(inventory);
        return new[] { inventory };
    }
    
}