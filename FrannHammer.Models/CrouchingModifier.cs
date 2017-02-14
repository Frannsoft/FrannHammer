using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// Whether or not the attacker is crouching.  Might be able to be a bool in the future.
    /// </summary>
    [Obsolete]
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
}