using System.Linq;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public abstract class SearchPredicateService
    {
        protected readonly RangeMatchProcessingService RangeMatchProcessingService;

        protected SearchPredicateService()
        {
            RangeMatchProcessingService = new RangeMatchProcessingService();
        }

        protected virtual bool ProcessData(RangeModel frameRange, string rawData)
        {
            var splits = rawData.Split('-');

            return splits.Any() && ProcessWhenThereIsParseableData(frameRange, splits);
        }

        protected bool ProcessWhenThereIsParseableData(RangeModel frameRange, string[] dataSplits)
        {
            if (dataSplits.Length == 1)
            {
                return new HitboxSearchPredicateProcessingService().ProcessWhenSplitsLengthIsOne(frameRange,
                    dataSplits);
            }

            if (dataSplits.Length > 1)
            {
                return ProcessWhenMoreThanOneSplitsResults(frameRange, dataSplits);
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

        protected virtual bool ProcessWhenHitboxLengthGreaterThanZero(RangeModel frameRange, int startValueFromDb,
            int endValueFromDb)
        {
            return RangeMatchProcessingService.Check(frameRange, startValueFromDb, endValueFromDb);
        }
    }
}
