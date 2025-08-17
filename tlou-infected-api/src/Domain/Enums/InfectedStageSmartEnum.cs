namespace tlou_infected_api.Domain.Enums;

public abstract class InfectedStageSmartEnum
{
    public static readonly InfectedStageSmartEnum Runner = new RunnerStage();
    public static readonly InfectedStageSmartEnum Stalker = new StalkerStage();
    public static readonly InfectedStageSmartEnum Clicker = new ClickerStage();
    public static readonly InfectedStageSmartEnum Bloater = new BloaterStage();
    public static readonly InfectedStageSmartEnum Shambler = new ShamblerStage();
    public static readonly InfectedStageSmartEnum RatKing = new RatKingStage();

    public abstract string InfectionTime { get; }
    public abstract int InfectionStage { get; }

    private InfectedStageSmartEnum() { }

    private class RunnerStage : InfectedStageSmartEnum
    {
        public override string InfectionTime => "2 Days";
        public override int InfectionStage => 1;
    }

    private class StalkerStage : InfectedStageSmartEnum
    {
        public override string InfectionTime => "2 weeks and 1 month";
        public override int InfectionStage => 2;
    }
    private class ClickerStage : InfectedStageSmartEnum
    {
        public override string InfectionTime => "1 Year";
        public override int InfectionStage => 2;
    } 
    
    private class BloaterStage : InfectedStageSmartEnum
    {
        public override string InfectionTime => "10 to 20 years";
        public override int InfectionStage => 2;
    }
    
    private class ShamblerStage : InfectedStageSmartEnum
    {
        public override string InfectionTime => "22 Years";
        public override int InfectionStage => 2;
    }
    
    private class RatKingStage : InfectedStageSmartEnum
    {
        public override string InfectionTime => "30 Years";
        public override int InfectionStage => 2;
    }
}