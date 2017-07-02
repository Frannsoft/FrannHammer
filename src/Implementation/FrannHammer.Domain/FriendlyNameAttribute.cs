using System;
using FrannHammer.Utility;

namespace FrannHammer.Domain
{
    /// <summary>
    /// Can be used as a Json-friendly name for the deserialized output of the assigned Property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FriendlyNameAttribute : System.Attribute
    {
        public string Name { get; }

        public FriendlyNameAttribute(string name)
        {
            Guard.VerifyStringIsNotNullOrEmpty(name, nameof(name));
            Name = name;
        }
    }
}
