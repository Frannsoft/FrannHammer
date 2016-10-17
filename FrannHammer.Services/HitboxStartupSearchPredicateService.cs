using System;
using FrannHammer.Core;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class HitboxStartupSearchPredicateService : SearchPredicateService
    {
        private readonly RangeMatchProcessingService _rangeMatchProcessingService;

        public HitboxStartupSearchPredicateService()
        {
            _rangeMatchProcessingService = new RangeMatchProcessingService();

            _rangeMatchProcessingService.ConfigureIsBetweenCheck(
                        (procService, frameRange, startValueFromDb, endValueFromDb) =>
                            procService.IsBetween(frameRange.StartValue, startValueFromDb, endValueFromDb) ||
                            procService.IsBetween(frameRange.EndValue, startValueFromDb, endValueFromDb));

            _rangeMatchProcessingService.ConfigureIsGreaterThanCheck(
                (procService, frameRange, startValueFromDb) =>
                        procService.IsGreaterThan(startValueFromDb, frameRange.StartValue));

            _rangeMatchProcessingService.ConfigureIsLessThanCheck(
                (procService, frameRange, startValueFromDb) =>
                        procService.IsLessThan(startValueFromDb, frameRange.StartValue));

            _rangeMatchProcessingService.ConfigureIsGreaterThanOrEqualToCheck(
                (procService, frameRange, startValueFromDb) =>
                        procService.IsGreaterThanOrEqualTo(startValueFromDb, frameRange.StartValue));

            _rangeMatchProcessingService.ConfigureIsLessThanOrEqualToCheck(
                (procService, frameRange, startValueFromDb) =>
                        procService.IsLessThanOrEqualTo(startValueFromDb, frameRange.StartValue));

            _rangeMatchProcessingService.ConfigureIsEqualToCheck(
               (procService, frameRange, startValueFromDb) =>
                       procService.IsEqualTo(frameRange.StartValue, startValueFromDb));
        }

        public Func<Hitbox, bool> GetHitboxStartupPredicate(RangeModel hitboxStartupFrame)
           => h => IsValueInHitboxRange(h.Hitbox1, hitboxStartupFrame) ||
               IsValueInHitboxRange(h.Hitbox2, hitboxStartupFrame) ||
               IsValueInHitboxRange(h.Hitbox3, hitboxStartupFrame) ||
               IsValueInHitboxRange(h.Hitbox4, hitboxStartupFrame) ||
               IsValueInHitboxRange(h.Hitbox5, hitboxStartupFrame) ||
               IsValueInHitboxRange(h.Hitbox6, hitboxStartupFrame);

        public bool IsValueInHitboxRange(string hitboxRaw, RangeModel frameRange)
        {
            Guard.VerifyObjectNotNull(frameRange, nameof(frameRange));
            return !string.IsNullOrEmpty(hitboxRaw) && ProcessHitboxData(frameRange, hitboxRaw);
        }

        protected override bool ProcessWhenHitboxLengthGreaterThanZero(RangeModel frameRange, int startValueFromDb,
            int endValueFromDb)
        {
            return _rangeMatchProcessingService.Check(frameRange, startValueFromDb, endValueFromDb);
        }
    }
}
