using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Domain.Common;
using tlou_infected_api.Domain.DTO.Inventory;
using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
public class InventoryController (InventoryService service) : ControllerBase
{
    /// <summary>
    /// Creates a survivor's inventory.
    /// </summary>
    /// <returns>The created inventory</returns>
    /// <response code="200">Inventory processed successfully</response>
    [HttpPost("/inventory")]
    public async Task<ActionResult<string>> CreateSurvivorInventory([FromBody]CreateInventorySurvivorDto createInventorySurvivor)
    {
        var joined = await service.CreateInventoryAndGetJoinedAsync(createInventorySurvivor);
        return Ok(joined);
    }
    
    /// <summary>
    /// Retrieves the inventory of all survivors.
    /// </summary>
    /// <returns>The inventory information for all survivors</returns>
    /// <response code="200">Returns the survivors' inventory</response>
    [HttpGet("/survivors/inventory")]
    public async Task<ActionResult<IEnumerable<InventorySurvivor>>> GetAllSurvivorInventories([FromQuery] PaginationParameters parameters)
    {
        var inventory = await service.GetPaginatedInventorySurvivor(parameters);
        return Ok(inventory);
    }
    
    /// <summary>
    /// Update the inventory of all survivors.
    /// </summary>
    /// <returns> Update The inventory information for all survivors</returns>
    /// <response code="200">Returns the survivors' inventory updated</response>
    [HttpPatch("/survivors/inventory")]
    public async Task<ActionResult<InventorySurvivor>> UpdateSurvivorInventory(InventorySurvivor inventory)
    {
        var updatedInventory = await service.UpdateInventorySurvivors(inventory);
        return Ok(updatedInventory);
    }
    
}