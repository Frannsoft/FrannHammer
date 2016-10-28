using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class AutoCancelSearchPredicateService : SearchPredicateService
    {
        public AutoCancelSearchPredicateService()
        {
            OverrideRangeChecks();
        }

        private void OverrideRangeChecks()
        {
            RangeMatchProcessingService.ConfigureIsBetweenCheck(
                (procService, frameRange, numberRange) =>
                    procService.IsGreaterThan(numberRange.Start, frameRange.StartValue) &&
                    (!numberRange.End.HasValue || procService.IsLessThan(numberRange.End.Value, frameRange.EndValue)));

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

        public Func<Autocancel, bool> GetAutoCancelSearchPredicate(RangeModel autoCancelFrame)
        {
            if (autoCancelFrame == null)
            { return null; }

            return a => IsValueInRange(a.Cancel1?.Replace(">", string.Empty), autoCancelFrame) ||
                    IsValueInRange(a.Cancel2?.Replace(">", string.Empty), autoCancelFrame);
        }
    }
}
