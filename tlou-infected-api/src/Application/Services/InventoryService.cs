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
        
        var insertedId = survivorInventory.Id ?? throw new InvalidOperationException("Id do inventário não foi gerado."); // usa o Id definido na entidade após a inserção para filtrar a agregação
        var joined = await inventoryRepository.JoinAndAggregateAsync(insertedId);
        return joined;
    }
    
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