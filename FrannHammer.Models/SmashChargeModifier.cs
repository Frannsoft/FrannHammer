using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// The smash charge modifier.  Default is for holding a standard charge smash rather than no charge smash.  
    /// </summary>
    [Obsolete]
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
}