using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// Whether or not the attack is electric.  
    /// Depending on whether or not more factors come out for this in the future it can be a bool instead.
    /// </summary>
    [Obsolete]
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
}