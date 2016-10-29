using System;
using System.Collections.Generic;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class DataParsingService
    {
        private const string Comma = ",";
        private const string Hyphen = "-";
        private const string Space = " ";
        private readonly IList<NumberRange> _numberRanges;

        public DataParsingService()
        {
            _numberRanges = new List<NumberRange>();
        }

        public IList<NumberRange> Parse(string rawData)
        {
            //if data is empty don't bother parsing it.
            if (string.IsNullOrEmpty(rawData))
            { return _numberRanges; }

            if (_numberRanges.Count > 0)
            { _numberRanges.Clear(); } //empty existing entries so we don't get stale data


            rawData = rawData.Replace(Space, string.Empty); //remove spaces
            if (rawData.Contains(Comma))
            {
                if (rawData.Contains(Hyphen))
                {
                    var commaSplits = rawData.Split(new[] { Comma }, StringSplitOptions.None); //multiple hitboxes, multiple frames active
                    ProcessMultipleHitboxMultipleFramesActive(commaSplits);
                }
                else
                {
                    var commaSplits = rawData.Split(new[] { Comma }, StringSplitOptions.None); //multi hitboxes, single frame actives

                    ProcessMultipleHitboxesSingleFramesActive(commaSplits);
                }
            }
            else
            {
                if (rawData.Contains(Hyphen))
                {
                    var rawRange = rawData.Split(new[] { Hyphen }, StringSplitOptions.None); //single value, multi frames active

                    ProcessSingleValueMultipleFramesActive(rawRange);
                }
                else
                {
                    ProcessSingleValueSingleFrameActive(rawData);
                }
            }
            return _numberRanges;
        }

        private void ProcessSingleValueSingleFrameActive(string rawValue)
        {
            int value;
            int.TryParse(rawValue, out value); //single value, single frame active
            _numberRanges.Add(new NumberRange(value));
        }

        private void ProcessSingleValueMultipleFramesActive(IReadOnlyList<string> rawRange)
        {
            if (rawRange.Count == 2)
            {
                int startRange, endRange;
                if (int.TryParse(rawRange[0], out startRange) && int.TryParse(rawRange[1], out endRange))
                {
                    _numberRanges.Add(new NumberRange(startRange, endRange));
                }
            }
        }

        private void ProcessMultipleHitboxMultipleFramesActive(IEnumerable<string> commaSplits)
        {
            foreach (var commaSplit in commaSplits)
            {
                var rawRange = commaSplit.Split(new[] { Hyphen }, StringSplitOptions.None);
                if (rawRange.Length == 2)
                {
                    int startRange, endRange;
                    if (int.TryParse(rawRange[0], out startRange) && int.TryParse(rawRange[1], out endRange))
                    {
                        _numberRanges.Add(new NumberRange(startRange, endRange));
                    }
                }
            }
        }

        private void ProcessMultipleHitboxesSingleFramesActive(IEnumerable<string> commaSplits)
        {
            foreach (var hyphenSplit in commaSplits)
            {
                int number;
                if (int.TryParse(hyphenSplit, out number))
                {
                    _numberRanges.Add(new NumberRange(number));
                }
            }
        }
    }
}
