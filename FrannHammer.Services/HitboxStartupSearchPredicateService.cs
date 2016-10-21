using System;
using FrannHammer.Core;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class HitboxStartupSearchPredicateService : SearchPredicateService
    {
        public HitboxStartupSearchPredicateService()
        {
            RangeMatchProcessingService.ConfigureIsBetweenCheck(
                        (procService, frameRange, startValueFromDb, endValueFromDb) =>
                            procService.IsBetween(frameRange.StartValue, startValueFromDb, endValueFromDb) ||
                            procService.IsBetween(frameRange.EndValue, startValueFromDb, endValueFromDb));

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

        public Func<Hitbox, bool> GetHitboxStartupPredicate(RangeModel hitboxStartupFrame)
           => h => IsValueInRange(h.Hitbox1, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox2, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox3, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox4, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox5, hitboxStartupFrame) ||
               IsValueInRange(h.Hitbox6, hitboxStartupFrame);

        public bool IsValueInRange(string hitboxRaw, RangeModel frameRange)
        {
            Guard.VerifyObjectNotNull(frameRange, nameof(frameRange));

            var dataParsingService = new DataParsingService();

            var dbNumberRanges = dataParsingService.Parse(hitboxRaw);

            if (dbNumberRanges.Count > 0)
            {
                if (dbNumberRanges[0].End.HasValue)
                {
                    return RangeMatchProcessingService.Check(frameRange, dbNumberRanges[0].Start,
                        dbNumberRanges[0].End.Value);
                }
                else
                {
                    return RangeMatchProcessingService.Check(frameRange, dbNumberRanges[0].Start);
                }
            }
            else
            {
                return false;
            }
        }
    }
}
