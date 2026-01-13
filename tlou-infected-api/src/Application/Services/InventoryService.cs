using MongoDB.Bson;
using tlou_infected_api.Domain.Common;
using tlou_infected_api.Domain.DTO.Inventory;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Application.Services;


public class InventoryService (IInventoryRepository inventoryRepository)
{
    
    public async Task<List<BsonDocument>> CreateInventoryAndGetJoinedAsync(CreateInventorySurvivorDto createInventorySurvivorDto)
    {
        var survivorInventory = createInventorySurvivorDto.BuildInventorySurvivor();
        await inventoryRepository.AddAsync(survivorInventory);
        
        var joined = await inventoryRepository.JoinAndAggregateAsync(); // Executa a agregação que realiza o join e retorna os documentos agregados
        return joined;
    }
    
    // public async Task<InventorySurvivor> CreateInventory(CreateInventorySurvivorDto createInventorySurvivorDto)
    // {
    //     var survivorInventory = createInventorySurvivorDto.BuildInventorySurvivor();
    //     
    //     await inventoryRepository.AddAsync(survivorInventory);
    //     return survivorInventory;
    // }
    
    public async Task<PagedResult<InventorySurvivor>> GetPaginatedInventorySurvivor(PaginationParameters parameters)
    {
        var survivorInventoryPaged = await inventoryRepository.GetPaginatedAsyncInventorySurvivor(parameters);
        return survivorInventoryPaged;
    }
    
    public async Task<IEnumerable<InventorySurvivor>> UpdateInventorySurvivors(InventorySurvivor inventory)
    {
        await inventoryRepository.UpsertInventory(inventory);
        return new[] { inventory };
    }
    
}