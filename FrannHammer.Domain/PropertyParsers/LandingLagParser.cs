using System.Collections.Generic;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.PropertyParsers
{
    public class LandingLagParser : PropertyParser
    {
        public override IDictionary<string, string> Parse(string rawData)
        {
            return Parse(rawData, results =>
            {
                int frames;
                string notesDataStrippedFromFrameValue = SeparateNotesDataFromHitbox(results, rawData);

                if (!int.TryParse(notesDataStrippedFromFrameValue, out frames))
                {
                    frames = 0;
                }

                results[FramesKey] = frames.ToString();
                return results;
            }, FramesKey, NotesKey);
        }
    }
}