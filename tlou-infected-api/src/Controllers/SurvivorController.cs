using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Data;
using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Controllers;

[Route("api/[controller]")]
public class SurvivorController : ControllerBase
{
    private readonly IMongoRepository<Survivor> _survivorRepository; 
    private readonly SurvivorService _service;
    private readonly IMongoCollection<Survivor>? _survivorService;

    public SurvivorController(AppDbContext appDbContext)
    {
        _survivorService = appDbContext.Database?.GetCollection<Survivor>("survivor");
        _service = new SurvivorService(appDbContext);
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
        return survivor != null ? Ok(survivor) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> Create(SurvivorDto createSurvivorDto)
    {
        var survivor = await _service.Create(createSurvivorDto);
        return CreatedAtAction(nameof(GetById),  new { id = survivor.Id }, survivor);
    }

    [HttpPut]
    public async Task<ActionResult> Update(SurvivorDto createSurvivorDto)
    {
        var success = await _service.UpdateSurvivor(createSurvivorDto);
        return success ? Ok(createSurvivorDto) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var sucess = await _service.DeleteSurvivor(id);
        return sucess ? Ok() : NotFound();
    }
}