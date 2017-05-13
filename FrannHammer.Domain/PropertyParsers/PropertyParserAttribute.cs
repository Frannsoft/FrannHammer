using System;
using FrannHammer.Utility;

namespace FrannHammer.Domain.PropertyParsers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyParserAttribute : Attribute
    {
        public Type ParserType { get; }

        public PropertyParserAttribute(Type parserType)
        {
            Guard.VerifyObjectNotNull(parserType, nameof(parserType));

            if (!parserType.IsSubclassOf(typeof(PropertyParser)))
            {
                throw new InvalidOperationException(
                    $"Specified parser type must derive from {nameof(PropertyParser)}.  '{parserType.Name}' does not.");
            }

            ParserType = parserType;
        }
    }
}
