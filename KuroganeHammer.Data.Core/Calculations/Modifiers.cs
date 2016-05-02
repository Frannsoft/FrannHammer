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

    public enum HitlagModifierType
    {
        Crouching,
        NotCrouching
    }

    public enum ShieldAdvantageModifier
    {
        Regular,
        Projectile_NotHitlag
    }
}
