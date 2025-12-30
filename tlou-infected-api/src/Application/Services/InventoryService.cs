using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.DTO.Inventory;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Application.Services;


public class InventoryService (IMongoRepository<InventorySurvivor> inventoryRepository)
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
    
    // TODO: Implement upsert Method.
    public async Task<InventorySurvivor> UpdatePartialSurvivorInventory(CreateInventorySurvivorDto createInventorySurvivor)
    {
        var inventorySurvivor = createInventorySurvivor.BuildInventorySurvivor();
        
        if (inventorySurvivor.Id == null)
        {
            await inventoryRepository.AddAsync(inventorySurvivor);
        }
        else
        {
            await inventoryRepository.UpdateAsync(inventorySurvivor.Id, inventorySurvivor);
        }
        
        return inventorySurvivor;
    }
    
}