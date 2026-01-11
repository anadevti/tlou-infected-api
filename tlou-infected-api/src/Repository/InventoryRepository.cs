using MongoDB.Driver;
using tlou_infected_api.Domain.Common;
using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Repository;

public class InventoryRepository : MongoRepository<InventorySurvivor>, IInventoryRepository
{
    private readonly IMongoCollection<InventorySurvivor> _collection;

    public InventoryRepository(IMongoDatabase database) : base(database)
    {
        _collection = database.GetCollection<InventorySurvivor>("InventorySurvivors");
    }

    public async Task UpsertInventory(InventorySurvivor inventory)
    {
        var filter = Builders<InventorySurvivor>.Filter.Eq(x => x.Id, inventory.Id);
        var update = Builders<InventorySurvivor>.Update
            .Set(x => x.Brick, inventory.Brick)
            .Set(x => x.Flamethrower, inventory.Flamethrower)
            .Set(x => x.Knife, inventory.Knife)
            .Set(x => x.MedicalKit, inventory.MedicalKit)
            .Set(x => x.Pills, inventory.Pills)
            .SetOnInsert(x => x.IsActive, inventory.IsActive);

        var upsert = new UpdateOneModel<InventorySurvivor>(filter, update)
        {
            IsUpsert = true
        };

        var options = new BulkWriteOptions { IsOrdered = false };
        await _collection.BulkWriteAsync(new[] { upsert }, options);
    }

    public async Task<PagedResult<InventorySurvivor>> GetPaginatedAsyncInventorySurvivor(PaginationParameters parameters)
    {
        var filter = Builders<InventorySurvivor>.Filter.Eq(x => x.IsActive, true);
    
        var totalCount = await _collection.CountDocumentsAsync(filter);

        var items = await _collection
            .Find(filter)
            .Skip(parameters.Skip)
            .Limit(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<InventorySurvivor>
        {
            Items = items,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            TotalCount = (int)totalCount
        };
    }
}