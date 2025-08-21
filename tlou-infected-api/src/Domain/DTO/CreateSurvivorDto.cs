namespace tlou_infected_api.Domain.DTO;

public class CreateSurvivorDto
{
    public int Life { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public string MainWeapon { get; set; }
    public int Stealth { get; set; }
}