using Microsoft.AspNetCore.Mvc;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Domain.DTO;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FactionController(FactionService service) : ControllerBase
{ 
        /// <summary>
        /// Retrieves all factions.
        /// </summary>
        /// <returns>A list of all factions</returns>
        /// <response code="200">Returns the list of factions</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Faction>>> GetFactions()
        {
            var getFaction = await service.GetAllFactions();
            return Ok(getFaction);
        }
    
        /// <summary>
        /// Retrieves a specific faction by its ID.
        /// </summary>
        /// <param name="id">The faction's unique identifier</param>
        /// <returns>The faction with the specified ID</returns>
        /// <response code="200">Returns the faction</response>
        /// <response code="404">Faction not found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Infected?>> GetFactionById(string id)
        {
            var faction = await service.GetFactionById(id);
            return Ok(faction);
        }
    
        /// <summary>
        /// Creates a new faction.
        /// </summary>
        /// <param name="createFactionDto">The faction data to create</param>
        /// <returns>The created faction</returns>
        /// <response code="201">Faction created successfully</response>
        [HttpPost]
        public async Task<ActionResult> CreateFaction(CreateFactionDto createFactionDto)
        {
            var createFaction = await service.Create(createFactionDto);
            return CreatedAtAction(nameof(GetFactionById), new { createFaction.Id }, createFaction);
        }
    
        /// <summary>
        /// Updates an existing faction.
        /// </summary>
        /// <param name="faction">The faction data to update</param>
        /// <returns>The updated faction data</returns>
        /// <response code="200">Faction updated successfully</response>
        [HttpPut]
        public async Task<ActionResult> Update(CreateFactionDto createFactionDto)
        {
            var updateFaction = await service.UpdateFaction(createFactionDto);
            return Ok(updateFaction);
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
            var deleteFaction = await service.DeleteFaction(id);
            return Ok(deleteFaction);
        }
}