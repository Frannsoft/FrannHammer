namespace FrannHammer.Models
{
    public class TrainingModeKnockbackProblemData
    {
        public double VictimPercent { get; set; }
        public int BaseDamage { get; set; }
        public double TargetWeight { get; set; }
        public double KnockbackGrowth { get; set; }
        public int BaseKnockbackSetKnockback { get; set; }
        public double StanceModifier { get; set; }
    }

    public class RageProblemData
    {
        public double AttackerPercent { get; set; }
    }

    public class VersusModeKnockbackProblemData
    {
        public double AttackerPercent { get; set; }
        public double VictimPercent { get; set; }
        public double BaseDamage { get; set; }
        public double TargetWeight { get; set; }
        public double KnockbackGrowth { get; set; }
        public int BaseKnockbackSetKnockback { get; set; }
        public double StanceModifier { get; set; }
        public double StaleMoveMultiplier { get; set; }
    }

    public class HitlagProblemData
    {
        public double Damage { get; set; }
        public double HitlagMultiplier { get; set; }
        public ElectricModifier ElectricModifier { get; set; }
        public CrouchingModifier CrouchingModifier { get; set; }
    }

    public class ShieldAdvantageProblemData
    {
        public int HitFrame { get; set; }
        public int FirstActionableFrame { get; set; }
        public int ShieldStun { get; set; }
        public ShieldAdvantageModifier ShieldAdvantageModifier { get; set; }
        //public int ShieldHitlag { get; set; }
        public HitlagProblemData HitlagData { get; set; }
        public int Hitlag { get; set; }
    }

    public class LedgeIntangiblityProblemData
    {
        public int AirborneFrames { get; set; }
        public int CharacterPercentage { get; set; }
    }

    public class ReboundDurationProblemData
    {
        public int Damage { get; set; }
    }

    public class GrabDurationProblemData
    {
        public int TargetPercent { get; set; }
        public ControllerInput Input { get; set; }
    }

    public class PikminGrabDurationProblemData
    {
        public int TargetPercent { get; set; }
        public PikminGrabControllerInput Input { get; set; }
    }

    public class SmashChargeProblemData
    {
        public int Damage { get; set; }
        public int HeldFrames { get; set; }
        public SmashChargeModifier SmashChargeModifier { get; set; } = SmashChargeModifier.Default;
    }
}
