using System;

namespace FrannHammer.Core.Calculations
{
    /// <summary>
    /// Handles all calculations dealing with actionable move data.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Returns the knockback as if in training mode.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public double TrainingModeKnockback(TrainingModeKnockbackProblemData data)
        {
            var result = (((data.VictimPercent + data.BaseDamage) / 10 + (data.VictimPercent + data.BaseDamage) * data.BaseDamage / 20) * (200 / (data.TargetWeight + 100)) * 1.4 + 18) * (data.KnockbackGrowth / 100)
                            + data.BaseKnockbackSetKnockback * data.StanceModifier;

            return result;
        }

        /// <summary>
        /// Returns the knockback as if in versus mode.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public double VersusModeKnockback(VersusModeKnockbackProblemData data)
        {
            var result = ((((data.VictimPercent + data.BaseDamage * data.StaleMoveMultiplier) / 10 +
                               (data.VictimPercent + data.BaseDamage * data.StaleMoveMultiplier) * data.BaseDamage *
                               (1 - (1 - data.StaleMoveMultiplier) * 0.3) / 20)
                              * 1.4 * (200 / (data.TargetWeight + 100)) + 18) * (data.KnockbackGrowth / 100) + data.BaseKnockbackSetKnockback) *
                            data.StanceModifier * Rage(new RageProblemData { AttackerPercent = data.AttackerPercent });

            return result;
        }

        /// <summary>
        /// Returns the calculated rage.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public double Rage(RageProblemData data)
        {
            double result = 1;

            if (data.AttackerPercent > 35 &&
                data.AttackerPercent < 150)
            {
                result = 1 + (data.AttackerPercent - 35) * (1.15 - 1) / (150 - 35);
            }

            return result;
        }

        /// <summary>
        /// Returns the calculated Hitstun.
        /// </summary>
        /// <param name="knockbackGrowth"></param>
        /// <returns></returns>
        public int Hitstun(double knockbackGrowth)
        {
            return (int)Math.Floor(knockbackGrowth * 0.4) - 1;
        }

        /// <summary>
        /// Returns the calculated Normal Shield stun.
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public int ShieldStunNormal(double damage)
        {
            return (int)Math.Floor(damage / 1.72 + 3.0 - 1.0);
        }

        /// <summary>
        /// Returns the calculated Power shielded Shield stun.
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public int ShieldStunPowerShield(double damage)
        {
            return (int)Math.Floor(damage / 2.61 + 3 - 1);
        }

        /// <summary>
        /// Returns the calculated Projectile Shield stun.
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public int ShieldStunProjectile(double damage)
        {
            return (int)Math.Floor(damage / 3.5 + 3 - 1);
        }

        /// <summary>
        /// Returns the calculated Power shielded projectile Shield stun.
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public int ShieldStunPowerShieldProjectile(double damage)
        {
            return (int)Math.Floor(damage / 5.22 + 3 - 1);
        }

        /// <summary>
        /// Returns the calculated Hitlag.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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

        ///// <summary>
        ///// Returns the calculated Hitlag.
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public double HitlagRaw(HitlagProblemData data)
        //{
        //    var retVal = (data.Damage / 2.6 + 5) *
        //       data.ElectricModifier.GetModifierValue() *
        //       data.HitlagMultiplier *
        //       data.CrouchingModifier.GetModifierValue() - 1;

        //    if (retVal > 30)
        //    {
        //        retVal = 30;
        //    }

        //    return retVal;
        //}

        /// <summary>
        /// Returns the calculated LedgeIntangibility.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int LedgeIntangibility(LedgeIntangiblityProblemData data)
        {
            if (data.AirborneFrames > 300)
            { throw new Exception($"{nameof(data.AirborneFrames)} cannot be higher than 300."); }

            if (data.CharacterPercentage > 120)
            { throw new Exception($"{nameof(data.CharacterPercentage)} cannot be higher than 120."); }

            var result = (int)Math.Floor(data.AirborneFrames * 0.2 + 64 - data.CharacterPercentage * 44 / 120.0);

            if (result < 24)
            { result = 24; }

            if (result > 124)
            { result = 124; }

            return result;
        }

        /// <summary>
        /// Returns the calculated shield advantage.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int ShieldAdvantage(ShieldAdvantageProblemData data)
        {
            var shieldAdvantage = data.HitFrame - (data.FirstActionableFrame - 1) + data.ShieldStun;

            if (data.HitlagData == null)
            { throw new Exception($"{nameof(data.HitlagData)} cannot be null."); }

            switch (data.ShieldAdvantageModifier)
            {
                case ShieldAdvantageModifier.ProjectileNotHitlag:
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

        /// <summary>
        /// Returns the calculated grab duration frames.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int GrabDuration(GrabDurationProblemData data)
        {
            return (int)(Math.Floor(90 + data.TargetPercent * 1.7) - data.Input.GetModifierValue());
        }

        /// <summary>
        /// Returns the calculated Pikmin grab durations frames.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int PikminGrabDuration(PikminGrabDurationProblemData data)
        {
            return (int)(Math.Floor(360.0 - data.TargetPercent) - data.Input.GetModifierValue());
        }

        /// <summary>
        /// Returns the calculated Smash charge frames.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
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
