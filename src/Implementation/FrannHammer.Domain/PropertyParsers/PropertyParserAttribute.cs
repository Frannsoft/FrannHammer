using System;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;

namespace FrannHammer.Domain.PropertyParsers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PropertyParserAttribute : Attribute
    {
        public Type ParserType { get; }
        public string MatchingPropertyName { get; }

        /// <summary>
        /// Used to map a specific <see cref="PropertyParser"/> to a property.  Primary use case is on <see cref="IMove"/>
        /// implementations.
        /// </summary>
        /// <param name="parserType"></param>
        /// <param name="matchingPropertyName"></param>
        public PropertyParserAttribute(Type parserType, string matchingPropertyName = "")
        {
            Guard.VerifyObjectNotNull(parserType, nameof(parserType));

            if (!parserType.IsSubclassOf(typeof(PropertyParser)))
            {
                throw new InvalidOperationException(
                    $"Specified parser type must derive from {nameof(PropertyParser)}.  '{parserType.Name}' does not.");
            }

            ParserType = parserType;
            MatchingPropertyName = matchingPropertyName;
        }
    }
}
