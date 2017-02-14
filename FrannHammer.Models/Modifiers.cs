using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// What the attack victim is doing when hit.
    /// </summary>
    [Obsolete]
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
}
