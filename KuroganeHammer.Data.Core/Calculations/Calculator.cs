namespace KuroganeHammer.Data.Core.Calculations
{
    public class Calculator
    {
        public double TrainingModeKnockback(double victimPercent, int baseDamage, double targetWeight,
            double knockbackGrowth, int baseKnockbackSetKnockback, double stanceModifier)
        {
            double result = 0;

            result = (((victimPercent + baseDamage) / 10 + (victimPercent + baseDamage) * baseDamage / 20) * (200 / (targetWeight + 100)) * 1.4 + 18) * (knockbackGrowth / 100)
            + baseKnockbackSetKnockback * stanceModifier;

            return result;
        }

        public double VersusModeKnockback(double victimPercent, int baseDamage, double staleMoveMultiplier,
            double targetWeight, double knockbackGrowth, int baseKnockbackSetKnockback, double stanceModifier)
        {
            var result = ((((victimPercent + baseDamage * staleMoveMultiplier) / 10 +
                               (victimPercent + baseDamage * staleMoveMultiplier) * baseDamage *
                               (1 - (1 - staleMoveMultiplier) * 0.3) / 20)
                              * 1.4 * (200 / (targetWeight + 100)) + 18) * (knockbackGrowth / 100) + baseKnockbackSetKnockback) *
                            stanceModifier * Rage(victimPercent);

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
    }
}
