using System;

namespace FrannHammer.Models
{
    /// <summary>
    /// Used to contain the actual value being used in the Core enum modifiers.
    /// </summary>
    [Obsolete]
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
            Value = value;
        }
    }
}