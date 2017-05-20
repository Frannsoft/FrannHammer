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

                if (!int.TryParse(rawData, out frames))
                {
                    frames = 0;
                }

                results[FramesKey] = frames.ToString();
                return results;
            }, FramesKey);
        }
    }
}