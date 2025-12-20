using Microsoft.AspNetCore.Mvc;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
public class SurvivorController(SurvivorService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Survivor>>> Get()
    {
        var survivor = await service.GetAll();
        return Ok(survivor);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Survivor?>> GetById(string id)
    {
        var survivor = await service.GetSurvivorById(id);
        return Ok(survivor);
    }
    
    [HttpGet("/survivors/{id}/status")]
    public async Task<ActionResult<string>> GetSurvivorStatus(string id)
    {
        var status = await service.GetSurvivorStatus(id);
        return Ok(status);
    }
    
    [HttpGet("/survivors/inventory")]
    public async Task<ActionResult<string>> GetSurvivorInventory()
    {
        var inventory = await service.GetSurvivorInventory();
        return Ok(inventory);
    }
    

    [HttpPost]
    public async Task<ActionResult> Create(SurvivorDto createSurvivorDto)
    {
        var survivor = await service.Create(createSurvivorDto);
        return CreatedAtAction(nameof(GetById),  new { id = survivor.Id }, survivor);
    }

    [HttpPut]
    public async Task<ActionResult> Update(SurvivorDto createSurvivorDto)
    {
        var success = await service.UpdateSurvivor(createSurvivorDto);
        return success ? Ok(createSurvivorDto) : UnprocessableEntity();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var sucess = await service.DeleteSurvivor(id);
        return sucess ? Ok() : NoContent();
    }
}