using System;
using FrannHammer.Core;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class SearchPredicateService
    {
        protected readonly RangeMatchProcessingService RangeMatchProcessingService;

        protected internal SearchPredicateService()
        {
            RangeMatchProcessingService = new RangeMatchProcessingService();
            InitializeDefaultRangeChecks();
        }

        private void InitializeDefaultRangeChecks()
        {
            RangeMatchProcessingService.ConfigureIsBetweenCheck(
                (procService, frameRange, numberRange) =>
                    procService.IsBetween(frameRange.StartValue, frameRange.EndValue, numberRange));

            RangeMatchProcessingService.ConfigureIsGreaterThanCheck(
               (procService, frameRange, startValueFromDb) =>
                       procService.IsGreaterThan(startValueFromDb, frameRange.StartValue));

            RangeMatchProcessingService.ConfigureIsLessThanCheck(
                (procService, frameRange, startValueFromDb) =>
                        procService.IsLessThan(startValueFromDb, frameRange.StartValue));

            RangeMatchProcessingService.ConfigureIsGreaterThanOrEqualToCheck(
                (procService, frameRange, startValueFromDb) =>
                        procService.IsGreaterThanOrEqualTo(startValueFromDb, frameRange.StartValue));

            RangeMatchProcessingService.ConfigureIsLessThanOrEqualToCheck(
                (procService, frameRange, startValueFromDb) =>
                        procService.IsLessThanOrEqualTo(startValueFromDb, frameRange.StartValue));

            RangeMatchProcessingService.ConfigureIsEqualToCheck(
               (procService, frameRange, startValueFromDb) =>
                       procService.IsEqualTo(frameRange.StartValue, startValueFromDb));
        }

        protected internal Func<T, bool> GetPredicate<T>(RangeModel frameRange)
            where T : BaseMoveHitboxMeta
        {
            if(frameRange == null)
            { return null; }

            return h => IsValueInRange(h.Hitbox1, frameRange) ||
                 IsValueInRange(h.Hitbox2, frameRange) ||
                 IsValueInRange(h.Hitbox3, frameRange) ||
                 IsValueInRange(h.Hitbox4, frameRange) ||
                 IsValueInRange(h.Hitbox5, frameRange) ||
                 IsValueInRange(h.Hitbox6, frameRange);
        }

        protected internal bool IsValueInRange(string raw, RangeModel frameRange)
        {
            Guard.VerifyObjectNotNull(frameRange, nameof(frameRange));

            var dataParsingService = new DataParsingService();

            var dbNumberRanges = dataParsingService.Parse(raw);

            if (dbNumberRanges.Count > 0)
            {
                if (dbNumberRanges[0].End.HasValue)
                {
                    return RangeMatchProcessingService.Check(frameRange, dbNumberRanges[0]);
                }
                else
                {
                    return RangeMatchProcessingService.Check(frameRange, dbNumberRanges[0]);
                }
            }
            else
            {
                return false;
            }
        }
    }
}
