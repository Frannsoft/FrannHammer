using System;

namespace FrannHammer.Models
{
    [Obsolete("Calculate functionality is Obsolete.")]
    public class TrainingModeKnockbackProblemData
    {
        public double VictimPercent { get; set; }
        public int BaseDamage { get; set; }
        public double TargetWeight { get; set; }
        public double KnockbackGrowth { get; set; }
        public int BaseKnockbackSetKnockback { get; set; }
        public double StanceModifier { get; set; }
    }
}
