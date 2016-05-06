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
                result = 1 + (percent - 35) * (1.15 - 1) / (150 - 35);
            }

            return result;
        }

        public int Hitstun(double knockbackGrowth)
        {
            return (int)Math.Floor(knockbackGrowth * 0.4) - 1;
        }

        public int ShieldStunNormal(double damage)
        {
            return (int)Math.Floor(damage / 1.72 + 3.0 - 1.0);
        }

        public int ShieldStunPowerShield(double damage)
        {
            return (int)Math.Floor(damage / 2.61 + 3 - 1);
        }

        public int ShieldStunProjectile(double damage)
        {
            return (int)Math.Floor(damage / 3.5 + 3 - 1);
        }

        public int ShieldStunPowerShieldProjectile(double damage)
        {
            return (int)Math.Floor(damage / 5.22 + 3 - 1);
        }

        public int Hitlag(HitlagProblemData data)
        {
            var retVal = (int)Math.Floor((data.Damage / 2.6 + 5) *
                data.ElectricModifier.GetModifierValue() *
                data.HitlagMultiplier *
                data.CrouchingModifier.GetModifierValue()) - 1;

            if (retVal > 30)
            {
                retVal = 30;
            }

            return retVal;
        }

        public double HitlagRaw(HitlagProblemData data)
        {
            var retVal = (data.Damage / 2.6 + 5) *
               data.ElectricModifier.GetModifierValue() *
               data.HitlagMultiplier *
               data.CrouchingModifier.GetModifierValue() - 1;

            if (retVal > 30)
            {
                retVal = 30;
            }

            return retVal;
        }

        public int LedgeIntangibility(LedgeIntangiblityProblemData data)
        {
            if (data.AirborneFrames > 300)
            { throw new Exception($"{nameof(data.AirborneFrames)} cannot be higher than 300.");}

            if (data.CharacterPercentage > 120)
            { throw new Exception($"{nameof(data.CharacterPercentage)} cannot be higher than 120.");}

            var result = (int)Math.Floor(data.AirborneFrames*0.2 + 64 - data.CharacterPercentage*44/120.0);

            if (result < 24)
            { result = 24; }

            if (result > 124)
            { result = 124; }

            return result;
        }

        public int ShieldAdvantage(ShieldAdvantageProblemData data)
        {
            var shieldAdvantage = data.HitFrame - (data.FirstActionableFrame - 1) + data.ShieldStun;

            if (data.HitlagData == null)
            { throw new Exception($"{nameof(data.HitlagData)} cannot be null."); }

            switch (data.ShieldAdvantageModifier)
            {
                case ShieldAdvantageModifier.Projectile_NotHitlag:
                    {
                        var hitlag = Hitlag(data.HitlagData);
                        shieldAdvantage = shieldAdvantage + hitlag;//(int)(Convert.ToDouble(shieldAdvantage) + Math.Round(hitlag));
                        break;
                    }
                case ShieldAdvantageModifier.Regular:
                    {
                        //shieldAdvantage = shieldAdvantage - hitlag;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return shieldAdvantage;
        }

        public int ReboundDuration(ReboundDurationProblemData data)
        {
            throw new NotImplementedException(
                "Waiting on response from SpaceJam on this.  Not sure if the KH formula is good.");
            //return (int) Math.Floor(data.Damage*15/8 + 7.5);
        }

        public int GrabDuration(GrabDurationProblemData data)
        {
            return (int) (Math.Floor(90 + data.TargetPercent*1.7) - data.Input.GetModifierValue());
        }

        public int PikminGrabDuration(PikminGrabDurationProblemData data)
        {
            return (int)(Math.Floor(360.0 - data.TargetPercent) - data.Input.GetModifierValue());
        }

        public int SmashCharge(SmashChargeProblemData data)
        {
            return (int)Math.Round(data.Damage * (data.HeldFrames / data.SmashChargeModifier.GetModifierValue()));
        }

        public double StaleMoveNegationMultiplier(StaleMoveNegationMultipler multiplier)
        {
            throw new NotImplementedException("Contact SpaceJam to explain this one.");
        }
    }
}
