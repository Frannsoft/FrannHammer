using System.Collections.Generic;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.PropertyParsers
{
    public class AutocancelParser : PropertyParser
    {
        public override IDictionary<string, string> Parse(string rawData)
        {
            return Parse(rawData, results =>
            {
                //I don't want to ref System.Web (for httputility) just for this call.
                string replacedRawData = rawData.Replace("&gt;", ">");

                var splitData = replacedRawData.Split(',');

                if (splitData.Length > 0)
                {
                    results[Cancel1Key] = SeparateNotesDataFromHitbox(results, splitData[0]);
                }
                if (splitData.Length > 1)
                {
                    results[Cancel2Key] = SeparateNotesDataFromHitbox(results, splitData[1]);
                }

                return results;
            }, Cancel1Key, Cancel2Key, NotesKey);
        }
    }
}