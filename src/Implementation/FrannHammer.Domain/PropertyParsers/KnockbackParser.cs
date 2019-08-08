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
            if (!string.IsNullOrEmpty(rawKbk) && rawKbk.Contains(primaryKnockbackValueChar))
            {
                var baseKbksRegexMatch = Regex.Match(rawKbk, regexPattern).Value;
                var trimmed = baseKbksRegexMatch.Trim(trimValues);
                return base.Parse(trimmed);
            }
            else
            {
                return base.Parse(rawKbk);
            }
        }
    }
}