namespace tlou_infected_api.Domain.DTO.Inventory;

public class CreateInventorySurvivorDto
{
    public string? Id { get; set; }
    public string? SurvivorId { get; set; }
    public string? MedicalKit {get; set;}
    public string? Brick {get; set;}
    public string? Knife {get; set;}
    public string? Flamethrower {get; set;}
    public string? Pills { get; set; }
    public bool IsActive {get; set;}
}