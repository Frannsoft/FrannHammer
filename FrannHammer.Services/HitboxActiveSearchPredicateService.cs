using System;
using FrannHammer.Core;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class HitboxActiveSearchPredicateService : SearchPredicateService
    {
        private readonly Func<RangeQuantifierService, RangeModel, int, int, bool> _equalToCheck;

        public HitboxActiveSearchPredicateService()
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

            _equalToCheck = (rangeQuantifierService, frameRange, startValueFromDb, endValueFromDb) =>
                rangeQuantifierService.IsBetween(frameRange.StartValue, startValueFromDb, endValueFromDb) ||
                rangeQuantifierService.IsEqualTo(frameRange.StartValue, startValueFromDb);
        }


        public Func<Hitbox, bool> GetHitboxActivePredicate(RangeModel hitboxActiveOnFrame)
            => h => IsValueInRange(h.Hitbox1, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox2, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox3, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox4, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox5, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox6, hitboxActiveOnFrame);

        public bool IsValueInRange(string hitboxRaw, RangeModel frameRange)
        {
            Guard.VerifyObjectNotNull(frameRange, nameof(frameRange));
            return !string.IsNullOrEmpty(hitboxRaw) && ProcessData(frameRange, hitboxRaw);
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
                    return RangeMatchProcessingService.Check(frameRange, startValueFromDb, endValueFromDb);
                }
            }
        }
    }
}
