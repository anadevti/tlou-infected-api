using Microsoft.AspNetCore.Mvc;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Domain.DTO;

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
    public async Task<ActionResult<string>> CreateSurvivorInventory(CreateInventorySurvivorDto createInventorySurvivor)
    {
        var createInventory = await service.CreateInventory(createInventorySurvivor);
        return Ok(createInventory);
    }
}