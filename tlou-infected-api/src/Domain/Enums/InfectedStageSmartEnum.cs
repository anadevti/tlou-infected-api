namespace tlou_infected_api.Domain.Enums;

public abstract class InfectedStageSmartEnum
{
    public static readonly InfectedStageSmartEnum Runner = new RunnerStage();
    public static readonly InfectedStageSmartEnum Stalker = new StalkerStage();

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
        public override string InfectionTime => "7 Days";
        public override int InfectionStage => 2;
    }   
}
