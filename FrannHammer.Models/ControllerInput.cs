using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// ControllerInput used.  Typically used with mashing out or determining grab duration.
    /// </summary>
    [Obsolete]
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
}