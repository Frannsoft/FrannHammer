using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FrannHammer.Domain.PropertyParsers
{
    public abstract class KnockbackParser : HitboxParser
    {
        protected IDictionary<string, string> AddBaseSetKnockbackCore(string rawKbk, string regexPattern, char[] trimValues,
            char primaryKnockbackValueChar, Func<string, bool> doesRawContainKnockBackValue)
        {
            if (rawKbk.Contains(primaryKnockbackValueChar))
            {
                var baseKbksRegexMatch = Regex.Match(rawKbk, regexPattern).Value;
                var trimmed = baseKbksRegexMatch.Trim(trimValues);
                return base.Parse(trimmed);
            }

            if (!doesRawContainKnockBackValue(rawKbk))
            {
                return base.Parse(rawKbk);
            }

            return base.Parse(rawKbk);
        }
    }
}