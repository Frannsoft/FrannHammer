using System;
using FrannHammer.Core;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class HitboxActiveSearchPredicateService : SearchPredicateService
    {
        private readonly RangeMatchProcessingService _rangeMatchProcessingService;

        private readonly Func<RangeQuantifierService, RangeModel, int, int, bool> _equalToCheck;

        public HitboxActiveSearchPredicateService()
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

            _equalToCheck = (rangeQuantifierService, frameRange, startValueFromDb, endValueFromDb) =>
                rangeQuantifierService.IsBetween(frameRange.StartValue, startValueFromDb, endValueFromDb) ||
                rangeQuantifierService.IsEqualTo(frameRange.StartValue, startValueFromDb);
        }


        public Func<Hitbox, bool> GetHitboxActivePredicate(RangeModel hitboxActiveOnFrame)
            => h => IsValueInHitboxRange(h.Hitbox1, hitboxActiveOnFrame) ||
               IsValueInHitboxRange(h.Hitbox2, hitboxActiveOnFrame) ||
               IsValueInHitboxRange(h.Hitbox3, hitboxActiveOnFrame) ||
               IsValueInHitboxRange(h.Hitbox4, hitboxActiveOnFrame) ||
               IsValueInHitboxRange(h.Hitbox5, hitboxActiveOnFrame) ||
               IsValueInHitboxRange(h.Hitbox6, hitboxActiveOnFrame);

        public bool IsValueInHitboxRange(string hitboxRaw, RangeModel frameRange)
        {
            Guard.VerifyObjectNotNull(frameRange, nameof(frameRange));
            return !string.IsNullOrEmpty(hitboxRaw) && ProcessHitboxData(frameRange, hitboxRaw);
        }

        protected override bool ProcessWhenHitboxLengthGreaterThanZero(RangeModel frameRange, int startValueFromDb, int endValueFromDb)
        {
            var rangeQuantifierService = new RangeQuantifierService();

            //annoying special case since we need to check the actual numbers inside the range of hitboxes during the active hitbox
            switch (frameRange.RangeQuantifier)
            {
                case RangeQuantifier.EqualTo:
                {
                    return _equalToCheck(rangeQuantifierService, frameRange, startValueFromDb, endValueFromDb);
                }
                case RangeQuantifier.GreaterThanOrEqualTo:
                {
                    return _equalToCheck(rangeQuantifierService, frameRange, startValueFromDb, endValueFromDb) ||
                           rangeQuantifierService.IsGreaterThan(startValueFromDb, frameRange.StartValue);
                }
                case RangeQuantifier.LessThanOrEqualTo:
                {
                        return _equalToCheck(rangeQuantifierService, frameRange, startValueFromDb, endValueFromDb) ||
                               rangeQuantifierService.IsLessThan(startValueFromDb, frameRange.StartValue);
                    }
                default:
                {
                    return _rangeMatchProcessingService.Check(frameRange, startValueFromDb, endValueFromDb);
                }
            }
        }
    }
}
