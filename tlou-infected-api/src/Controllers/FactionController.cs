using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FactionController : ControllerBase
{
        private readonly IMongoCollection<Faction>? _factionCollection;
    
        public FactionController(AppDbContext appDbContext)
        {
            _factionCollection = appDbContext.Database?.GetCollection<Faction>("faction");
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faction>>> Get()
        {
            return await _factionCollection.Find(FilterDefinition<Faction>.Empty).ToListAsync();
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<Infected?>> GetById(string id)
        {
            var faction = await _factionCollection.Find(factionId => factionId.Id == id).FirstOrDefaultAsync();
            return faction is not null ? Ok(faction) : NotFound();
        }
    
        [HttpPost]
        public async Task<ActionResult> Create(CreateFactionDto createFactionDto)
        {
            var faction = new Faction
            {
                FactionType = createFactionDto.FactionType,
                FactionStatus = createFactionDto.FactionStatus,
                Territory = createFactionDto.Territory
            };
            await _factionCollection.InsertOneAsync(faction);
            return CreatedAtAction(nameof(GetById),  new { id = faction.Id }, faction);
        }
    
        [HttpPut]
        public async Task<ActionResult> Update(Faction faction)
        {
            var filter = Builders<Faction>.Filter.Eq(f => f.Id, faction.Id);
            var updateedFaction = await _factionCollection.ReplaceOneAsync(filter, faction);
            return Ok(updateedFaction);
        }
    
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Faction>.Filter.Eq(f => f.Id, id);
            await _factionCollection.DeleteOneAsync(filter);
            return Ok();
        }
}