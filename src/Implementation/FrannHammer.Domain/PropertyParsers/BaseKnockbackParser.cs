using System;
using System.Collections.Generic;
using System.Linq;

namespace FrannHammer.Domain.PropertyParsers
{
    public class BaseKnockbackParser : KnockbackParser
    {
        public override IDictionary<string, string> Parse(string rawData)
        {
            const string matchBaseKnockbackRegex = "(B: ).[^W]*";
            char[] baseKnockbackTrimValues = { 'B', ':', ' ' };
            const char primaryKnockbackValueChar = 'B';
            Func<string, bool> doesRawContainBaseKnockbackValue = raw => raw.Contains('W') && !raw.Contains('B') ||
                                                                         !rawData.Contains('B') && !rawData.Contains('W');

            return AddBaseSetKnockbackCore(rawData, matchBaseKnockbackRegex, baseKnockbackTrimValues,
                primaryKnockbackValueChar,
                doesRawContainBaseKnockbackValue);
        }
    }
}