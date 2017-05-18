using System;
using System.Collections.Generic;
using System.Linq;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.PropertyParsers
{
    public abstract class PropertyParser
    {
        public abstract IDictionary<string, string> Parse(string rawData);

        private static IDictionary<string, string> CreateDefaultResults(params string[] defaultKeys)
        {
            return defaultKeys.ToDictionary(key => key, key => string.Empty);
        }

        protected IDictionary<string, string> Parse(string rawData,
            Func<IDictionary<string, string>, IDictionary<string, string>> customParsingOperation,
            params string[] defaultKeys)
        {
            var results = CreateDefaultResults(defaultKeys);

            if (string.IsNullOrEmpty(rawData) || rawData.Equals("-"))
            {
                results[RawValueKey] = string.Empty;
                return results;
            }
            else
            {
                results.Add(RawValueKey, rawData);
            }

            return customParsingOperation(results);
        }
    }
}
