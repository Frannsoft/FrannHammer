using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.Domain.PropertyParsers
{
    public class SetKnockbackParser : KnockbackParser
    {
        public override IDictionary<string, string> Parse(string rawData)
        {
            const string matchSetKnockbackRegex = "(W: ).[^B]*";
            char[] setKnockbackTrimValues = { 'W', ':', ' ' };
            const char primaryKnockbackValueChar = 'W';
            Func<string, bool> doesRawContainSetKnockbackValue = raw => rawData.Contains('B') &&
                                                                        !rawData.Contains('W') ||
                                                                        !rawData.Contains('B') &&
                                                                        !rawData.Contains('W');

            return AddBaseSetKnockbackCore(rawData, matchSetKnockbackRegex, setKnockbackTrimValues,
                primaryKnockbackValueChar,
                doesRawContainSetKnockbackValue);
        }
    }
}