using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Data;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InfectedController: ControllerBase
{
    private readonly IMongoCollection<Infected>? _infectedCollection;

    public InfectedController(AppDbContext appDbContext)
    {
        _infectedCollection = appDbContext.Database?.GetCollection<Infected>("infected");
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
    public async Task<ActionResult> Create(Infected infected)
    {
        await _infectedCollection.InsertOneAsync(infected);
        return CreatedAtAction(nameof(GetById),  new { id = infected.Id }, infected);
    }

    [HttpPut]
    public async Task<ActionResult> Update(Infected infected)
    {
        var filter = Builders<Infected>.Filter.Eq(f => f.Id, infected.Id);
        var updateedInfected = await _infectedCollection.ReplaceOneAsync(filter, infected);
        return Ok(updateedInfected);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var filter = Builders<Infected>.Filter.Eq(f => f.Id, id);
        await _infectedCollection.DeleteOneAsync(filter);
        return Ok();
    }
}