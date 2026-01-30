using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Domain.DTO.Survivor;

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
}