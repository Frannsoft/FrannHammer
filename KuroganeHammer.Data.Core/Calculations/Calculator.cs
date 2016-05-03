using System;

namespace KuroganeHammer.Data.Core.Calculations
{
    public class Calculator
    {
        public double TrainingModeKnockback(TrainingModeKnockbackProblemData data)
        {
            double result = 0;

            result = (((data.VictimPercent + data.BaseDamage) / 10 + (data.VictimPercent + data.BaseDamage) * data.BaseDamage / 20) * (200 / (data.TargetWeight + 100)) * 1.4 + 18) * (data.KnockbackGrowth / 100)
            + data.BaseKnockbackSetKnockback * data.StanceModifier;

            return result;
        }

        public double VersusModeKnockback(VersusModeKnockbackProblemData data)
        {
            var result = ((((data.VictimPercent + data.BaseDamage * data.StaleMoveMultiplier) / 10 +
                               (data.VictimPercent + data.BaseDamage * data.StaleMoveMultiplier) * data.BaseDamage *
                               (1 - (1 - data.StaleMoveMultiplier) * 0.3) / 20)
                              * 1.4 * (200 / (data.TargetWeight + 100)) + 18) * (data.KnockbackGrowth / 100) + data.BaseKnockbackSetKnockback) *
                            data.StanceModifier * Rage(data.VictimPercent);

            return result;
        }

        public double Rage(double percent)
        {

            double result = 0;

            if (percent > 35 &&
                percent < 150)
            {
                result = 1 + (percent - 35)*(1.15 - 1)/(150 - 35);
            }

            return result;
        }

        public int Hitstun(double knockbackGrowth)
        {
            return (int)Math.Floor(knockbackGrowth*0.4) - 1;
        }

        public int ShieldStunNormal(double damage)
        {
            return (int)Math.Floor(damage / 1.72 + 3.0 - 1.0);
        }
    }
}
