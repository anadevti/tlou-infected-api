namespace tlou_infected_api.Domain.DTO;

public class CreateFactionDto
{
    public string FactionType { get; set; }
    public bool FactionStatus { get; set; }
    public string Territory { get; set; }
}