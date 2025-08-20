using Ardalis.SmartEnum;

namespace tlou_infected_api.Domain.Enums;

public sealed  class InfectedStageSmartEnum: SmartEnum<InfectedStageSmartEnum>
{
    public static readonly InfectedStageSmartEnum Runner = new(nameof(Runner), 1, "2 Days", 1);
    public static readonly InfectedStageSmartEnum Stalker = new(nameof(Stalker), 2, "2 weeks and 1 month", 2);
    public static readonly InfectedStageSmartEnum Clicker = new(nameof(Clicker), 3, "1 Year", 3);
    public static readonly InfectedStageSmartEnum Bloater = new(nameof(Bloater), 4, "10 to 20 years", 4);
    public static readonly InfectedStageSmartEnum Shambler = new(nameof(Shambler), 5, "22 Years", 5);
    public static readonly InfectedStageSmartEnum RatKing = new(nameof(RatKing), 6, "30 Years", 6);

    public string InfectionTime { get; }
    public int InfectionStage { get; }

    private InfectedStageSmartEnum(string name, int value, string infectionTime, int infectionStage) 
        : base(name, value)
    {
        InfectionTime = infectionTime;
        InfectionStage = infectionStage;
    }
}