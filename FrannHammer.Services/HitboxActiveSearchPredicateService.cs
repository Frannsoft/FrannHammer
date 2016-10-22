using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class HitboxActiveSearchPredicateService : SearchPredicateService
    {
        public HitboxActiveSearchPredicateService()
        {
            OverrideRangeChecks();
        }

        private void OverrideRangeChecks()
        {
            RangeMatchProcessingService.ConfigureIsBetweenCheck(
                       (procService, frameRange, numberRange) =>
                           procService.IsBetween(frameRange.StartValue, numberRange) ||
                           procService.IsBetween(frameRange.EndValue, numberRange));

            RangeMatchProcessingService.ConfigureIsGreaterThanOrEqualToCheck(
                (procService, frameRange, startValueFromDb) =>
                    procService.IsGreaterThan(startValueFromDb, frameRange.StartValue) ||
                    procService.IsBetween(frameRange.StartValue, new NumberRange(startValueFromDb)) ||
                    procService.IsEqualTo(frameRange.StartValue, new NumberRange(startValueFromDb).Start));

            RangeMatchProcessingService.ConfigureIsLessThanOrEqualToCheck(
                (procService, frameRange, startValueFromDb) =>
                    procService.IsLessThan(startValueFromDb, frameRange.StartValue) ||
                    procService.IsBetween(frameRange.StartValue, new NumberRange(startValueFromDb)) ||
                    procService.IsEqualTo(frameRange.StartValue, new NumberRange(startValueFromDb).Start));

            RangeMatchProcessingService.ConfigureIsEqualToRangeCheck(
                (procService, frameRange, numberRange) =>
                procService.IsBetween(frameRange.StartValue, numberRange) ||
                procService.IsEqualTo(frameRange.StartValue, numberRange.Start));
        }

        public Func<Hitbox, bool> GetHitboxActivePredicate(RangeModel hitboxActiveOnFrame)
            => h => IsValueInRange(h.Hitbox1, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox2, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox3, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox4, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox5, hitboxActiveOnFrame) ||
               IsValueInRange(h.Hitbox6, hitboxActiveOnFrame);
    }
}
