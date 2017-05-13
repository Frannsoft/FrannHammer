using System.Collections.Generic;
using System.Text.RegularExpressions;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.PropertyParsers
{
    public class BaseDamageParser : PropertyParser
    {
        public override IDictionary<string, string> Parse(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            { return new Dictionary<string, string>(); }

            var splitData = rawData.Split('/');

            var retVal = new Dictionary<string, string>
            {
                [RawValueKey] = rawData
            };
            //hard copy of data prior to parsing

            if (!rawData.Contains("No") &&
                !rawData.Contains("Yes"))
            {
                SetHitboxesData(retVal, splitData);
            }

            return retVal;
        }

        private static string GetNoteDataFromHitboxActiveData(string rawHitboxData)
        {
            var match = Regex.Match(rawHitboxData, @"\(([^\)]+)\)");
            return match.Value;
        }

        private static string SeparateNotesDataFromHitbox(IDictionary<string, string> model, string rawData)
        {
            string noteData = GetNoteDataFromHitboxActiveData(rawData);

            if (!model.ContainsKey(NotesKey))
            {
                model[NotesKey] = noteData;
            }
            else
            {
                model[NotesKey] += noteData;
            }

            return !string.IsNullOrEmpty(noteData) ? rawData.Replace(noteData, string.Empty).Trim() : rawData;
        }

        private static void SetHitboxesData(IDictionary<string, string> model, string[] rawData)
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
