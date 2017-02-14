using System;

namespace FrannHammer.Models
{
    [Obsolete("Calculate functionality is Obsolete.")]
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
}