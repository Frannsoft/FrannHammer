using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// Controller input used when dealing with Pikmin grab duration.
    /// </summary>
    [Obsolete]
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
}