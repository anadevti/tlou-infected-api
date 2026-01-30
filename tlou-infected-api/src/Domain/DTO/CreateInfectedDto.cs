using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Domain.DTO;

public class InfectedDto
{
    public string? Id { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Weaknesses { get; set; }
}