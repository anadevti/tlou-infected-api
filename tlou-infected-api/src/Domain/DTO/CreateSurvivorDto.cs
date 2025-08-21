namespace tlou_infected_api.Domain.DTO;

public class CreateSurvivorDto
{
    public int Life { get; set; }
    public string Strength { get; set; }
    public string Agility { get; set; }
    public string MainWeapon { get; set; }
    public string Stealth { get; set; }
}