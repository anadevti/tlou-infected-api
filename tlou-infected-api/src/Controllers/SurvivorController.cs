using Microsoft.AspNetCore.Mvc;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
public class SurvivorController : ControllerBase
{
    private readonly IMongoRepository<Survivor> _survivorRepository; 
    
    public SurvivorController(IMongoRepository<Survivor> survivorRepository)
    {
        _survivorRepository = survivorRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Survivor>>> Get()
    {
        return await _survivorRepository.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Survivor?>> GetById(string id)
    {
        var survivor = _survivorRepository.GetByIdAsync(id);
        return Ok (survivor);
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
        await _survivorRepository.AddAsync(survivor);
        return CreatedAtAction(nameof(GetById),  new { id = survivor.Id }, survivor);
    }

    [HttpPut]
    public async Task<ActionResult> Update(Survivor survivor)
    {
        await _survivorRepository.UpdateAsync(survivor.Id, survivor);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _survivorRepository.DeleteAsync(id);
        return Ok();
    }
}