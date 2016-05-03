using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuroganeHammer.Data.Core.Calculations
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

    public class VersusModeKnockbackProblemData
    {
        public double VictimPercent { get; set; }
        public int BaseDamage { get; set; }
        public double TargetWeight { get; set; }
        public double KnockbackGrowth { get; set; }
        public int BaseKnockbackSetKnockback { get; set; }
        public double StanceModifier { get; set; }
        public double StaleMoveMultiplier { get; set; }
    }

    public class ShieldStunNormalProblemData
    {
        
    }
}
