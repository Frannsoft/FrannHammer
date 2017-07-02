using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.PropertyParsers
{
    public abstract class PropertyParser
    {
        public abstract IDictionary<string, string> Parse(string rawData);

        private static IDictionary<string, string> CreateDefaultResults(params string[] defaultKeys)
        {
            return defaultKeys.ToDictionary(key => key, key => string.Empty);
        }

        protected IDictionary<string, string> Parse(string rawData,
            Func<IDictionary<string, string>, IDictionary<string, string>> customParsingOperation,
            params string[] defaultKeys)
        {
            var results = CreateDefaultResults(defaultKeys);

            if (string.IsNullOrEmpty(rawData) || rawData.Equals("-"))
            {
                results[RawValueKey] = string.Empty;
                return results;
            }
            else
            {
                results.Add(RawValueKey, rawData);
            }

            return customParsingOperation(results);
        }

        private static string GetNoteDataFromHitboxActiveData(string rawHitboxData)
        {
            var match = Regex.Match(rawHitboxData, @"\(([^\)]+)\)");
            return match.Value;
        }

        protected static string SeparateNotesDataFromHitbox(IDictionary<string, string> model, string rawData)
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
    }
}
