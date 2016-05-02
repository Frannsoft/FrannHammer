using KuroganeHammer.Data.Core.Calculations;

namespace KuroganeHammer.Data.Api.Models
{
    public class CalculatorRequestModel
    {
        public double AttackerPercent { get; set; }
        public double VictimPercent { get; set; }
        public int BaseDamage { get; set; }
        public double TargetWeight { get; set; }
        public double KnockbackGrowth { get; set; }
        public int BaseKnockbackSetKnockback { get; set; }
        public double HitlagModifier { get; set; }
        public int HitFrame { get; set; }
        public int FirstActiveFrame { get; set; }
        public double Staleness { get; set; }
        public Modifiers Modifiers { get; set; }
        public ElectricModifier ElectricModifier { get; set; }
        public HitlagModifierType HitlagModifierType { get; set; }
        public ShieldAdvantageModifier ShieldAdvantageModifier { get; set; }
    }

    

    public class CalculatorResponseModel
    {
        public double Rage { get; set; }
        public double TrainingModeKnockback { get; set; }
        public double VersusModeKnockback { get; set; }
        public int HitstunFrames { get; set; }
        public int HitlagFrames { get; set; }
        public int ShieldAdvantage_Normal { get; set; }
        public int ShieldAdvantage_Powershield { get; set; }
        public int ShieldAdvantage_Projectile { get; set; }
        public int ShieldAdvantage_PowershieldProjectile { get; set; }
        public int ShieldStun_Normal { get; set; }
        public int ShieldStun_Powershield { get; set; }
        public int ShieldStun_Projectile { get; set; }
        public int ShieldStun_PowershieldProjectile { get; set; }
    }

}