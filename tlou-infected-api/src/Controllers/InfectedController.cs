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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Infected>>> Get()
    {
        return await _infectedCollection.Find(FilterDefinition<Infected>.Empty).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Infected?>> GetById(string id)
    {
        var infected = await _infectedCollection.Find(infectedId => infectedId.Id == id).FirstOrDefaultAsync();
        return infected is not null ? Ok(infected) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateInfectedDto createInfectedDto)
    {
        var infected = await _service.CreateInfected(createInfectedDto); // service infected sendo chamado
        return CreatedAtAction(nameof(GetById),  new { id = infected.Id }, infected);
    }

    [HttpPut]
    public async Task<ActionResult> Update(Infected infected)
    {
        var success = await _service.UpdateInfected(infected);
        return success ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id, Infected infected)
    {
        var success = await _service.DeleteInfected(id, infected);
        return success ? Ok() : NotFound();
    }
}