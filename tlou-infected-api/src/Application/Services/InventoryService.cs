using tlou_infected_api.Domain.DTO;
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
}