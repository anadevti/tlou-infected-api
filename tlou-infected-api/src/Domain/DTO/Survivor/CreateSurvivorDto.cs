using MongoDB.Bson;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Domain.DTO;

public class SurvivorDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Life { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public required string MainWeapon { get; set; }
    public int Stealth { get; set; }
    public SurvivorStatusEnum Status { get; set; }

    public Survivor BuildSurvivor()
    {
        var survivor = new Survivor
        {
            Id = Id,
            Name = Name,
            Life = Life,
            Strength = Strength,
            Agility = Agility,
            MainWeapon = MainWeapon,
            Stealth = Stealth,
            Status = Status
        };
        return survivor;
    }
}