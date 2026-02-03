using tlou_infected_api.Domain.DTO;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Application.Services;

public class FactionService (IMongoRepository<Faction> factionRepository)
{
    public async Task<Faction> Create(CreateFactionDto createFactionDto)
    {
        var faction = createFactionDto.BuildFaction();
        await factionRepository.AddAsync(faction);
        return faction;
    }
    
    public async Task<IEnumerable<Faction>> GetAllFactions()
    {
        return await factionRepository.GetAllAsync();
    }

    public async Task<Faction> GetFactionById(string id)
    {
        var faction = await factionRepository.GetByIdAsync(id);
        return faction;
    }
    
    public async Task<Faction> UpdateFaction(CreateFactionDto createFactionDto)
    {
        var faction = createFactionDto.BuildFaction();
        await factionRepository.UpdateAsync(createFactionDto.Id, faction);
        return faction;
    }

    public async Task<bool> DeleteFaction(string id)
    {
        await factionRepository.DeleteAsync(id);
        return true;
    }
}