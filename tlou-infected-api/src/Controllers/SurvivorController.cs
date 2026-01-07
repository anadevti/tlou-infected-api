using Microsoft.AspNetCore.Mvc;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.DTO.Survivor;
using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
public class SurvivorController(SurvivorService service) : ControllerBase
{
    /// <summary>
    /// Retrieves all survivors.
    /// </summary>
    /// <returns>A list of all survivors</returns>
    /// <response code="200">Returns the list of survivors</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Survivor>>> Get()
    {
        var survivor = await service.GetAll();
        return Ok(survivor);
    }

    /// <summary>
    /// Retrieves a specific survivor by their ID.
    /// </summary>
    /// <param name="id">The survivor's unique identifier</param>
    /// <returns>The survivor with the specified ID</returns>
    /// <response code="200">Returns the survivor</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Survivor?>> GetById(string id)
    {
        var survivor = await service.GetSurvivorById(id);
        return Ok(survivor);
    }
    
    /// <summary>
    /// Retrieves the status of a specific survivor.
    /// </summary>
    /// <param name="id">The survivor's unique identifier</param>
    /// <returns>The survivor's current status</returns>
    /// <response code="200">Returns the survivor's status</response>
    [HttpGet("/survivors/{id}/status")]
    public async Task<ActionResult<string>> GetSurvivorStatus(string id)
    {
        var status = await service.GetSurvivorStatus(id);
        return Ok(status);
    }

    /// <summary>
    /// Creates a new survivor.
    /// </summary>
    /// <param name="createSurvivorDto">The survivor data to create</param>
    /// <returns>The created survivor</returns>
    /// <response code="201">Survivor created successfully</response>
    [HttpPost]
    public async Task<ActionResult> Create(SurvivorDto createSurvivorDto)
    {
        var survivor = await service.Create(createSurvivorDto);
        return CreatedAtAction(nameof(GetById),  new { id = survivor.Id }, survivor);
    }

    /// <summary>
    /// Updates an existing survivor.
    /// </summary>
    /// <param name="createSurvivorDto">The survivor data to update</param>
    /// <returns>The updated survivor data</returns>
    /// <response code="200">Survivor updated successfully</response>
    /// <response code="422">Unable to process the survivor update</response>
    [HttpPut]
    public async Task<ActionResult> Update(SurvivorDto createSurvivorDto)
    {
        var success = await service.UpdateSurvivor(createSurvivorDto);
        return success ? Ok(createSurvivorDto) : UnprocessableEntity();
    }

    /// <summary>
    /// Deletes a survivor by their ID.
    /// </summary>
    /// <param name="id">The survivor's unique identifier</param>
    /// <returns>No content</returns>
    /// <response code="200">Survivor deleted successfully</response>
    /// <response code="204">Survivor not found</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var sucess = await service.DeleteSurvivor(id);
        return sucess ? Ok() : NoContent();
    }
}