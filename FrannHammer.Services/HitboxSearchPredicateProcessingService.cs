using System.Collections.Generic;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class HitboxSearchPredicateProcessingService
    {
        private readonly RangeQuantifierService _rangeQuantifierService;

        public HitboxSearchPredicateProcessingService()
        {
            _rangeQuantifierService = new RangeQuantifierService();
        }

        public bool ProcessWhenHitboxLengthNotGreaterThanZero(RangeModel frameRange, int startValueFromDb, string[] hitboxDataSplits)
        {
            var rangeQuantifierService = new RangeQuantifierService();

            switch (frameRange.RangeQuantifier)
            {
                case RangeQuantifier.Between:
                    {
                        return rangeQuantifierService.IsBetween(startValueFromDb, frameRange.StartValue, frameRange.EndValue);
                    }
                default:
                    {
                        return startValueFromDb == frameRange.StartValue;
                    }
            }
        }

        public bool ProcessWhenSplitsLengthIsOne(RangeModel frameRange, string[] hitboxDataSplits)
        {
            int startValueFromDb;
            if (int.TryParse(hitboxDataSplits[0], out startValueFromDb))
            {
                switch (frameRange.RangeQuantifier)
                {
                    case RangeQuantifier.Between:
                        {
                            return _rangeQuantifierService.IsBetween(startValueFromDb, frameRange.StartValue,
                                frameRange.EndValue);
                        }
                    default:
                        {
                            return startValueFromDb == frameRange.StartValue;
                        }
                }
            }
            else
            {
                //try splitting by comma
                var commaSplits = hitboxDataSplits[0].Split(',');

                if (commaSplits.Length > 1)
                {
                    var values = ParseAllNumbersInSplitFromCommaSeparation(commaSplits);
                    if (values.Count > 0)
                    {
                        var rangeQuantifierService = new RangeQuantifierService();
                        foreach (var value in values)
                        {
                            switch (frameRange.RangeQuantifier)
                            {
                                case RangeQuantifier.Between:
                                    {
                                        return rangeQuantifierService.IsBetween(value, frameRange.StartValue,
                                            frameRange.EndValue);
                                    }
                                default:
                                    {
                                        return value == frameRange.StartValue;
                                    }
                            }
                        }
                        return false; //didn't find any values that met the criteria
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false; //no number found to be able to compare
                }
            }
        }

        private IList<int> ParseAllNumbersInSplitFromCommaSeparation(string[] rawHitboxData)
        {
            var hitboxNumbers = new List<int>();

            foreach (var rawHitboxValue in rawHitboxData)
            {
                int tempNumber;
                if (int.TryParse(rawHitboxValue, out tempNumber))
                {
                    hitboxNumbers.Add(tempNumber);
                }
            }
            return hitboxNumbers;
        }
    }
}
