using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Domain.Entities;

public class Infected
{
    public string Id { get; set; }
    public InfectedStageSmartEnum Type { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Weaknesses { get; set; }
}