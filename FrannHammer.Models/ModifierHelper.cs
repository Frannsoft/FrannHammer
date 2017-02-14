using System;
using System.Linq;
using System.Reflection;

namespace FrannHammer.Models
{
    /// <summary>
    /// Small helper class to get the <see cref="ModifierValueAttribute"/> data from an <see cref="Enum"/>.
    /// </summary>
    [Obsolete]
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
}