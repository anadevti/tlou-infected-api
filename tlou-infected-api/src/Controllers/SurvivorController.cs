using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
public class SurvivorController : ControllerBase
{
    private readonly IMongoCollection<Survivor>? _survivorCollection;
    
    public SurvivorController(AppDbContext appDbContext)
    {
        _survivorCollection = appDbContext.Database?.GetCollection<Survivor>("survivor");
    }
    
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Survivor>>> Get()
    {
        return await _survivorCollection.Find(FilterDefinition<Survivor>.Empty).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Survivor?>> GetById(string id)
    {
        var survivor = await _survivorCollection.Find(Survivor => Survivor.Id == id).FirstOrDefaultAsync();
        return survivor is not null ? Ok(survivor) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateSurvivorDto createSurvivorDto)
    {
        var survivor = new Survivor
        {
            Life = createSurvivorDto.Life,
            Strength = createSurvivorDto.Strength,
            Agility = createSurvivorDto.Agility,
            MainWeapon = createSurvivorDto.MainWeapon,
            Stealth = createSurvivorDto.Stealth
        };
        await _survivorCollection.InsertOneAsync(Survivor);
        return CreatedAtAction(nameof(GetById),  new { id = survivor.Id }, survivor);
    }

    public Survivor Survivor { get; set; }

    [HttpPut]
    public async Task<ActionResult> Update(Survivor survivor)
    {
        var filter = Builders<Survivor>.Filter.Eq(f => f.Id, survivor.Id);
        var updateedSurvivor = await _survivorCollection.ReplaceOneAsync(filter, survivor);
        return Ok(updateedSurvivor);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var filter = Builders<Survivor>.Filter.Eq(f => f.Id, id);
        await _survivorCollection.DeleteOneAsync(filter);
        return Ok();
    }
    
}