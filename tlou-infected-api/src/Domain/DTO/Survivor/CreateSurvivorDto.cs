using FluentValidation;
using FluentValidation.Results;
using tlou_infected_api.Domain.Enums;

namespace tlou_infected_api.Domain.DTO.Survivor;

public class SurvivorDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Life { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public string MainWeapon { get; set; }
    public int Stealth { get; set; }
    public SurvivorStatusEnum Status { get; set; }
}

public sealed class SurvivorValidator : AbstractValidator<SurvivorDto>
{
    public SurvivorValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("O Campo Name não pode ser vazio.");
        
        RuleFor(x => x.Life)
            .GreaterThanOrEqualTo(1)
            .WithMessage("O Campo Life não pode ser menor que zero.");

        RuleFor(x => x.Strength)
            .GreaterThanOrEqualTo(1)
            .WithMessage("O Campo Strength não pode ser menor que zero.");

        RuleFor(x => x.Agility)
            .LessThanOrEqualTo(1)
            .WithMessage("O Campo Agility não pode ser menor que zero.");
        
        RuleFor(x => x.MainWeapon)
            .NotEmpty()
            .NotNull()
            .WithMessage("O Campo MainWeapon não pode ser vazio.");
    }
    
    public void ValidateSurvivor()
    {
        SurvivorDto survivor = new SurvivorDto();
        var validator = new SurvivorValidator();

        ValidationResult result = validator.Validate(survivor);
    }
}