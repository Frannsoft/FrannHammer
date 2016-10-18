using System;
using FrannHammer.Core;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class FirstActionableFrameSearchPredicateService : SearchPredicateService
    {
        public FirstActionableFrameSearchPredicateService()
        {
            //RangeMatchProcessingService.ConfigureIsBetweenCheck(
            //            (procService, frameRange, startValueFromDb, endValueFromDb) =>
            //                procService.IsBetween(frameRange.StartValue, startValueFromDb, endValueFromDb) ||
            //                procService.IsBetween(frameRange.EndValue, startValueFromDb, endValueFromDb));
            RangeMatchProcessingService.ConfigureIsBetweenCheck(
                (procService, frameRange, startValueFromDb, endValueFromDb) =>
                    procService.IsBetween(frameRange.StartValue, startValueFromDb, frameRange.EndValue,
                        endValueFromDb));

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

        public Func<Move, bool> GetFirstActionableFrameSearchPredicate(RangeModel firstActionableFrameRange)
           => move => IsValueInRange(move.FirstActionableFrame, firstActionableFrameRange);

        public bool IsValueInRange(string rawFrame, RangeModel frameRange)
        {
            Guard.VerifyObjectNotNull(frameRange, nameof(frameRange));
            return !string.IsNullOrEmpty(rawFrame) && ProcessData(frameRange, rawFrame);
        }

        protected override bool ProcessData(RangeModel frameRange, string rawData)
        {
            int parsedFrameValue;
            return int.TryParse(rawData, out parsedFrameValue) && RangeMatchProcessingService.Check(frameRange, parsedFrameValue, parsedFrameValue);
        }
    }
}
