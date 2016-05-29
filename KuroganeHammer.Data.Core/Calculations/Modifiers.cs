using System;
using System.Linq;
using System.Reflection;

namespace KuroganeHammer.Data.Core.Calculations
{
    /// <summary>
    /// Used to contain the actual value being used in the Core enum modifiers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ModifierValueAttribute : Attribute
    {
        /// <summary>
        /// The value of the modifier used in calculations.
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// Create a new <see cref="ModifierValueAttribute"/> using the passed in value.
        /// </summary>
        /// <param name="value"></param>
        public ModifierValueAttribute(double value)
        {
            Guard.VerifyObjectNotNull(value, nameof(value));
            Value = value;
        }
    }

    /// <summary>
    /// Small helper class to get the <see cref="ModifierValueAttribute"/> data from an <see cref="Enum"/>.
    /// </summary>
    public static class ModifierHelper
    {
        /// <summary>
        /// Get the value of the <see cref="ModifierValueAttribute"/> if it exists.
        /// </summary>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static double GetModifierValue(this Enum modifier)
        {
            var modifierName = modifier.ToString();
            var type = modifier.GetType();
            var memInfo = type.GetMember(modifierName);
            var valueAttribute = memInfo.Single(m => m.Name.Equals(modifierName)).GetCustomAttribute<ModifierValueAttribute>();

            return valueAttribute.Value;
        }
    }

    /// <summary>
    /// Determines which hitbox is having data resolved in the equation.  Really important at the 
    /// public request level in the api.  Otherwise it would be nightmare to act on all of them.
    /// </summary>
    public enum HitboxOptions
    {
        /// <summary>
        /// First hitbox.
        /// </summary>
        First,

        /// <summary>
        /// Second hitbox
        /// </summary>
        Second,

        /// <summary>
        /// Third hitbox
        /// </summary>
        Third,

        /// <summary>
        /// Fourth hitbox
        /// </summary>
        Fourth,

        /// <summary>
        /// Fifth hitbox
        /// </summary>
        Fifth
    }

    /// <summary>
    /// What the attack victim is doing when hit.
    /// </summary>
    public enum Modifiers
    {
        /// <summary>
        /// Just standing = 1.0
        /// </summary>
        [ModifierValue(1.0)]
        Standing,

        /// <summary>
        /// Charging a smash attack = 1.2
        /// </summary>
        [ModifierValue(1.2)]
        ChargingSmash,

        /// <summary>
        /// Crouching = 0.85
        /// </summary>
        [ModifierValue(0.85)]
        CrouchCancelling,

        /// <summary>
        /// Grounded Meteor = 0.8
        /// </summary>
        [ModifierValue(0.8)]
        GroundedMeteor
    }

    /// <summary>
    /// Whether or not the attack is electric.  
    /// Depending on whether or not more factors come out for this in the future it can be a bool instead.
    /// </summary>
    public enum ElectricModifier
    {
        /// <summary>
        /// A normal, non-elemental attack = 1.0
        /// </summary>
        [ModifierValue(1.0)]
        NormalAttack,

        /// <summary>
        /// An electric attack = 1.5
        /// </summary>
        [ModifierValue(1.5)]
        ElectricAttack
    }

    /// <summary>
    /// Whether or not the attacker is crouching.  Might be able to be a bool in the future.
    /// </summary>
    public enum CrouchingModifier
    {
        /// <summary>
        /// Is Crouching = 0.67
        /// </summary>
        [ModifierValue(0.67)]
        Crouching,

        /// <summary>
        /// Is not crouching = 1.0
        /// </summary>
        [ModifierValue(1.0)]
        NotCrouching
    }

    /// <summary>
    /// The type of shield advantage.
    /// </summary>
    public enum ShieldAdvantageModifier
    {
        /// <summary>
        /// Default.
        /// </summary>
        Regular,

        /// <summary>
        /// From projectile.
        /// </summary>
        ProjectileNotHitlag
    }

    /// <summary>
    /// ControllerInput used.  Typically used with mashing out or determining grab duration.
    /// </summary>
    public enum ControllerInput
    {
        /// <summary>
        /// Using the Left stick = 8.0
        /// </summary>
        [ModifierValue(8.0)]
        LStick,

        /// <summary>
        /// Using buttons = 14.3
        /// </summary>
        [ModifierValue(14.3)]
        Button
    }

    /// <summary>
    /// Controller input used when dealing with Pikmin grab duration.
    /// </summary>
    public enum PikminGrabControllerInput
    {
        /// <summary>
        /// Left stick = 7.0
        /// </summary>
        [ModifierValue(7.0)]
        LStick,

        /// <summary>
        /// Using buttons = 12.5
        /// </summary>
        [ModifierValue(12.5)]
        Button
    }
    
    /// <summary>
    /// The smash charge modifier.  Default is for holding a standard charge smash rather than no charge smash.  
    /// </summary>
    public enum SmashChargeModifier
    {
        /// <summary>
        /// Default = 150.
        /// </summary>
        [ModifierValue(150)]
        Default,

        /// <summary>
        /// Megaman's forward smash = 86.
        /// </summary>
        [ModifierValue(86)]
        MegamanFSmash
    }

    /// <summary>
    /// The staleness of a move.  The higher the level the more stale the move is.
    /// </summary>
    public enum StaleMoveNegationMultipler
    {
        /// <summary>
        /// S1 = 8.0
        /// </summary>
        [ModifierValue(8.000)]
        S1,

        /// <summary>
        /// S2 = 7.594
        /// </summary>
        [ModifierValue(7.594)]
        S2,

        /// <summary>
        /// S3 = 6.782
        /// </summary>
        [ModifierValue(6.782)]
        S3,

        /// <summary>
        /// S4 = 6.028
        /// </summary>
        [ModifierValue(6.028)]
        S4,

        /// <summary>
        /// S5 = 5.274
        /// </summary>
        [ModifierValue(5.274)]
        S5,

        /// <summary>
        /// S6 = 4.462
        /// </summary>
        [ModifierValue(4.462)]
        S6,

        /// <summary>
        /// S7 = 3.766
        /// </summary>
        [ModifierValue(3.766)]
        S7,

        /// <summary>
        /// S8 = 2.954
        /// </summary>
        [ModifierValue(2.954)]
        S8,

        /// <summary>
        /// S9 = 2.2
        /// </summary>
        [ModifierValue(2.200)]
        S9
    }
}
