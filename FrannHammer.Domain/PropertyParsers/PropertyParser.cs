using System.Collections.Generic;

namespace FrannHammer.Domain.PropertyParsers
{
    public abstract class PropertyParser
    {
        public abstract IDictionary<string, string> Parse(string rawData);
    }
}
