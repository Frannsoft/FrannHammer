namespace KuroganeHammer.Data.Core.Calculations
{
    public class Calculator
    {
        public double TrainingModeKnockback(double victimPercent, int baseDamage, double targetWeight,
            double knockbackGrowth, int baseKnockbackSetKnockback, double modifier)
        {
            double result = 0;

            result = (((victimPercent + baseDamage) / 10 + (victimPercent + baseDamage) * baseDamage / 20) * (200 / (targetWeight + 100)) * 1.4 + 18) * (knockbackGrowth / 100)
            + baseKnockbackSetKnockback * modifier;

            return result;
        }
    }
}
