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
    
        /// <summary>
        /// Retrieves all factions.
        /// </summary>
        /// <returns>A list of all factions</returns>
        /// <response code="200">Returns the list of factions</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faction>>> Get()
        {
            return await _factionCollection.Find(FilterDefinition<Faction>.Empty).ToListAsync();
        }
    
        /// <summary>
        /// Retrieves a specific faction by its ID.
        /// </summary>
        /// <param name="id">The faction's unique identifier</param>
        /// <returns>The faction with the specified ID</returns>
        /// <response code="200">Returns the faction</response>
        /// <response code="404">Faction not found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Infected?>> GetById(string id)
        {
            var faction = await _factionCollection.Find(factionId => factionId.Id == id).FirstOrDefaultAsync();
            return faction is not null ? Ok(faction) : NotFound();
        }
    
        /// <summary>
        /// Creates a new faction.
        /// </summary>
        /// <param name="createFactionDto">The faction data to create</param>
        /// <returns>The created faction</returns>
        /// <response code="201">Faction created successfully</response>
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
    
        /// <summary>
        /// Updates an existing faction.
        /// </summary>
        /// <param name="faction">The faction data to update</param>
        /// <returns>The updated faction data</returns>
        /// <response code="200">Faction updated successfully</response>
        [HttpPut]
        public async Task<ActionResult> Update(Faction faction)
        {
            var filter = Builders<Faction>.Filter.Eq(f => f.Id, faction.Id);
            var updateedFaction = await _factionCollection.ReplaceOneAsync(filter, faction);
            return Ok(updateedFaction);
        }
    
        /// <summary>
        /// Deletes a faction by its ID.
        /// </summary>
        /// <param name="id">The faction's unique identifier</param>
        /// <returns>No content</returns>
        /// <response code="200">Faction deleted successfully</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filter = Builders<Faction>.Filter.Eq(f => f.Id, id);
            await _factionCollection.DeleteOneAsync(filter);
            return Ok();
        }
}