using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Domain.DTO;

public class SurvivorDto
{
    public int Life { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public string MainWeapon { get; set; }
    public int Stealth { get; set; }

    public Survivor BuildSurvivor()
    {
        var survivor = new Survivor
        {
            Life = Life,
            Strength = Strength,
            Agility = Agility,
            MainWeapon = MainWeapon,
            Stealth = Stealth
        };
        return survivor;
    }
}