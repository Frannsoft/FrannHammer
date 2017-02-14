using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// Determines which hitbox is having data resolved in the equation.  Really important at the 
    /// public request level in the api.  Otherwise it would be nightmare to act on all of them.
    /// </summary>
    [Obsolete]
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
}