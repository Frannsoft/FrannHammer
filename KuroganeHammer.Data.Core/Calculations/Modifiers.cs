using System;
using System.Linq;
using System.Reflection;

namespace KuroganeHammer.Data.Core.Calculations
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ModifierValueAttribute : Attribute
    {
        public double Value { get; }

        public ModifierValueAttribute(double value)
        {
            Guard.VerifyObjectNotNull(value, nameof(value));
            Value = value;
        }
    }

    public static class ModifierHelper
    {
        public static double GetModifierValue(this Enum modifier)
        {
            var modifierName = modifier.ToString();
            var type = modifier.GetType();
            var memInfo = type.GetMember(modifierName);
            var valueAttribute = memInfo.Single(m => m.Name.Equals(modifierName)).GetCustomAttribute<ModifierValueAttribute>();

            return valueAttribute.Value;
        }
    }

    public enum Modifiers
    {
        [ModifierValue(1.0)]
        Standing,

        [ModifierValue(1.2)]
        ChargingSmash,

        [ModifierValue(0.85)]
        CrouchCancelling,

        [ModifierValue(0.8)]
        GroundedMeteor
    }

    public enum ElectricModifier
    {
        [ModifierValue(1.0)]
        NormalAttack,

        [ModifierValue(1.5)]
        ElectricAttack
    }

    public enum CrouchingModifier
    {
        [ModifierValue(0.67)]
        Crouching,

        [ModifierValue(1.0)]
        NotCrouching
    }

    public enum ShieldAdvantageModifier
    {
        Regular,
        Projectile_NotHitlag
    }

    public enum ControllerInput
    {
        [ModifierValue(8.0)]
        LStick,

        [ModifierValue(14.3)]
        Button
    }

    public enum PikminGrabControllerInput
    {
        [ModifierValue(7.0)]
        LStick,

        [ModifierValue(12.5)]
        Button
    }

    public enum SmashChargeModifier
    {
        [ModifierValue(150)]
        Default,

        [ModifierValue(86)]
        MegamanFSmash
    }

    public enum StaleMoveNegationMultipler
    {
        [ModifierValue(8.000)]
        S1,

        [ModifierValue(7.594)]
        S2,

        [ModifierValue(6.782)]
        S3,

        [ModifierValue(6.028)]
        S4,

        [ModifierValue(5.274)]
        S5,

        [ModifierValue(4.462)]
        S6,

        [ModifierValue(3.766)]
        S7,

        [ModifierValue(2.954)]
        S8,

        [ModifierValue(2.200)]
        S9
    }
}
