using System.Collections.Generic;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.PropertyParsers
{
    public class HitboxParser : PropertyParser
    {
        public override IDictionary<string, string> Parse(string rawData)
        {
            return Parse(rawData, results =>
             {
                 var splitData = rawData.Split('/');

                 //hard copy of data prior to parsing

                 if (!rawData.Contains("No") &&
                      !rawData.Contains("Yes"))
                 {
                     SetHitboxesData(results, splitData);
                 }

                 return results;
             }, Hitbox1Key, Hitbox2Key, Hitbox3Key, Hitbox4Key, Hitbox5Key);
        }

        protected static void SetHitboxesData(IDictionary<string, string> model, string[] rawData)
        {
            if (rawData.Length > 0)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[0]);
                model[Hitbox1Key] = hitboxActiveNoParens;
            }

            if (rawData.Length > 1)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[1]);
                model[Hitbox2Key] = hitboxActiveNoParens;
            }

            if (rawData.Length > 2)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[2]);
                model[Hitbox3Key] = hitboxActiveNoParens;
            }

            if (rawData.Length > 3)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[3]);
                model[Hitbox4Key] = hitboxActiveNoParens;
            }

            if (rawData.Length > 4)
            {
                string hitboxActiveNoParens = SeparateNotesDataFromHitbox(model, rawData[4]);
                model[Hitbox5Key] = hitboxActiveNoParens;
            }
        }
    }
}
