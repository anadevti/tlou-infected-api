using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Domain.DTO;

public class CreateFactionDto
{
    public string Id { get; set; }
    public string FactionType { get; set; }
    public bool FactionStatus { get; set; }
    public string Territory { get; set; }

    public Faction BuildFaction()
    {
        var faction = new Faction()
        {
            Id = Id,
            FactionType = FactionType,
            FactionStatus = FactionStatus,
            Territory = Territory
        };
        return faction;
    }
}