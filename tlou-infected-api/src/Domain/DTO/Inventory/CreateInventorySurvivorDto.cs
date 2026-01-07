using tlou_infected_api.Domain.Entities;

namespace tlou_infected_api.Domain.DTO.Inventory;

public class CreateInventorySurvivorDto
{
    public string? Id { get; set; }
    public string? MedicalKit {get; set;}
    public string? Brick {get; set;}
    public string? Knife {get; set;}
    public string? Flamethrower {get; set;}
    public string? Pills { get; set; }
    public bool IsActive {get; set;}
    
    public InventorySurvivor BuildInventorySurvivor()
    {
        var inventory = new InventorySurvivor
        {
            Id = Id,
            MedicalKit = MedicalKit,
            Brick = Brick,
            Knife = Knife,
            Flamethrower = Flamethrower,
            Pills = Pills,
            IsActive = IsActive
        };
        return inventory;
    }
    
}