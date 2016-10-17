using System.Linq;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public abstract class SearchPredicateService
    {
        protected abstract bool ProcessWhenHitboxLengthGreaterThanZero(RangeModel frameRange, int startValueFromDb,
            int endValueFromDb);

        protected bool ProcessHitboxData(RangeModel frameRange, string hitboxRawData)
        {
            var splits = hitboxRawData.Split('-');

            return splits.Any() && ProcessWhenThereIsParseableHitboxData(frameRange, splits);
        }

        protected bool ProcessWhenThereIsParseableHitboxData(RangeModel frameRange, string[] hitboxDataSplits)
        {
            if (hitboxDataSplits.Length == 1)
            {
                return new HitboxSearchPredicateProcessingService().ProcessWhenSplitsLengthIsOne(frameRange,
                    hitboxDataSplits);
            }

            if (hitboxDataSplits.Length > 1)
            {
                return ProcessWhenMoreThanOneSplitsResults(frameRange, hitboxDataSplits);
            }
            return false;
        }

        protected bool ProcessWhenMoreThanOneSplitsResults(RangeModel frameRange, string[] hitboxDataSplits)
        {
            int startValueFromDb, endValueFromDb;
            var hitboxSearchPredicateProcessingService = new HitboxSearchPredicateProcessingService();

            if (int.TryParse(hitboxDataSplits[0], out startValueFromDb) && int.TryParse(hitboxDataSplits[1], out endValueFromDb))
            {
                int hitboxLength = endValueFromDb - startValueFromDb;
                if (hitboxLength > 0)
                {
                    return ProcessWhenHitboxLengthGreaterThanZero(frameRange, startValueFromDb,
                        endValueFromDb);
                }
                else
                {
                    return hitboxSearchPredicateProcessingService.ProcessWhenHitboxLengthNotGreaterThanZero(frameRange,
                        startValueFromDb, hitboxDataSplits);
                }
            }
            else
            {
                return hitboxSearchPredicateProcessingService.ProcessWhenHitboxLengthNotGreaterThanZero(frameRange,
                        startValueFromDb, hitboxDataSplits);
            }
        }
    }
}
