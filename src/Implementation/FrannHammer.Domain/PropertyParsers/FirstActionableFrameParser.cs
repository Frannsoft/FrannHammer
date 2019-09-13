using System.Collections.Generic;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.PropertyParsers
{
    public class FirstActionableFrameParser : PropertyParser
    {
        public override IDictionary<string, string> Parse(string rawData)
        {
            return Parse(rawData, results =>
            {
                results[FrameKey] = SeparateNotesDataFromHitbox(results, rawData);
                return results;
            }, FrameKey, NotesKey);
        }
    }
}