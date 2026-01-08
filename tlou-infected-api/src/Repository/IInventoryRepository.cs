using tlou_infected_api.Domain.Common;
using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Repository;

public interface IInventoryRepository : IMongoRepository<InventorySurvivor>
{
    Task UpsertInventory(InventorySurvivor inventory);
    Task<PagedResult<InventorySurvivor>> GetPaginatedAsyncInventorySurvivor(PaginationParameters parameters);
}