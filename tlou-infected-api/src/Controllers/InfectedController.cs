using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.Enums;
using tlou_infected_api.Application.Services;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InfectedController: ControllerBase
{
    // constructors
    private readonly IMongoCollection<Infected>? _infectedCollection;
    private readonly InfectedService _service;

    public InfectedController(AppDbContext appDbContext)
    {
        _infectedCollection = appDbContext.Database?.GetCollection<Infected>("infected");
        _service = new InfectedService(appDbContext);
    }

    /// <summary>
    /// Retrieves all infected.
    /// </summary>
    /// <returns>A list of all infected</returns>
    /// <response code="200">Returns the list of infected</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Infected>>> Get()
    {
        return await _infectedCollection.Find(FilterDefinition<Infected>.Empty).ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific infected by their ID.
    /// </summary>
    /// <param name="id">The infected's unique identifier</param>
    /// <returns>The infected with the specified ID</returns>
    /// <response code="200">Returns the infected</response>
    /// <response code="404">Infected not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Infected?>> GetById(string id)
    {
        var infected = await _service.GetInfectedById(id);
        return infected != null ? Ok(infected) : NotFound();
    }

    /// <summary>
    /// Creates a new infected.
    /// </summary>
    /// <param name="infectedDto">The infected data to create</param>
    /// <returns>The created infected</returns>
    /// <response code="201">Infected created successfully</response>
    [HttpPost]
    public async Task<ActionResult> Create(InfectedDto infectedDto)
    {
        var infected = await _service.CreateInfected(infectedDto); // service infected sendo chamado
        return CreatedAtAction(nameof(GetById),  new { id = infected.Id }, infected);
    }

    /// <summary>
    /// Updates an existing infected.
    /// </summary>
    /// <param name="infectedDto">The infected data to update</param>
    /// <returns>The updated infected data</returns>
    /// <response code="200">Infected updated successfully</response>
    /// <response code="404">Infected not found</response>
    [HttpPut]
    public async Task<ActionResult> Update(InfectedDto infectedDto)
    {
        var success = await _service.UpdateInfected(infectedDto);
        return success ? Ok(infectedDto) : NotFound();
    }

    /// <summary>
    /// Deletes an infected by their ID.
    /// </summary>
    /// <param name="id">The infected's unique identifier</param>
    /// <returns>No content</returns>
    /// <response code="200">Infected deleted successfully</response>
    /// <response code="404">Infected not found</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var success = await _service.DeleteInfected(id);
        return success ? Ok() : NotFound();
    }
}