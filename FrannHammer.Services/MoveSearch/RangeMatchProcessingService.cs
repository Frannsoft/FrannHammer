using System;
using FrannHammer.Models;

namespace FrannHammer.Services.MoveSearch
{
    public class RangeMatchProcessingService
    {
        private Func<RangeQuantifierService, RangeModel, NumberRange, bool> _isBetweenCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isGreaterThanCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isGreaterThanOrEqualToCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isLessThanCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isLessThanOrEqualToCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isEqualToCheck;
        private Func<RangeQuantifierService, RangeModel, NumberRange, bool> _isEqualToRangeCheck;

        private readonly RangeQuantifierService _rangeQuantifierService;

        public RangeMatchProcessingService()
        {
            _rangeQuantifierService = new RangeQuantifierService();
        }

        internal void ConfigureIsBetweenCheck(Func<RangeQuantifierService, RangeModel, NumberRange, bool> isBetweenCheck) =>
            _isBetweenCheck = isBetweenCheck;

        internal void ConfigureIsEqualToRangeCheck(
                Func<RangeQuantifierService, RangeModel, NumberRange, bool> isEqualToRangeCheck) =>
            _isEqualToRangeCheck = isEqualToRangeCheck;

        internal void ConfigureIsGreaterThanCheck(Func<RangeQuantifierService, RangeModel, int, bool> isGreaterThanCheck) =>
            _isGreaterThanCheck = isGreaterThanCheck;

        internal void ConfigureIsLessThanCheck(Func<RangeQuantifierService, RangeModel, int, bool> isLessThanCheck) =>
            _isLessThanCheck = isLessThanCheck;

        internal void ConfigureIsGreaterThanOrEqualToCheck(Func<RangeQuantifierService, RangeModel, int, bool> isGreaterThanOrEqualToCheck) =>
            _isGreaterThanOrEqualToCheck = isGreaterThanOrEqualToCheck;

        internal void ConfigureIsLessThanOrEqualToCheck(Func<RangeQuantifierService, RangeModel, int, bool> isLessThanOrEqualToCheck) =>
          _isLessThanOrEqualToCheck = isLessThanOrEqualToCheck;

        internal void ConfigureIsEqualToCheck(Func<RangeQuantifierService, RangeModel, int, bool> isEqualToCheck) =>
            _isEqualToCheck = isEqualToCheck;


        public bool Check(RangeModel rangeModel, NumberRange numberRange)
        {
            switch (rangeModel.RangeQuantifier)
            {
                case RangeQuantifier.Between:
                    {
                        return _isBetweenCheck(_rangeQuantifierService, rangeModel, numberRange);
                    }
                case RangeQuantifier.LessThan:
                    {
                        return _isLessThanCheck(_rangeQuantifierService, rangeModel, numberRange.Start);
                    }
                case RangeQuantifier.LessThanOrEqualTo:
                    {
                        return _isLessThanOrEqualToCheck(_rangeQuantifierService, rangeModel, numberRange.Start);
                    }
                case RangeQuantifier.GreaterThan:
                    {
                        return _isGreaterThanCheck(_rangeQuantifierService, rangeModel, numberRange.Start);
                    }
                case RangeQuantifier.GreaterThanOrEqualTo:
                    {
                        return _isGreaterThanOrEqualToCheck(_rangeQuantifierService, rangeModel, numberRange.Start);
                    }
                case RangeQuantifier.EqualTo:
                    {
                        return _isEqualToRangeCheck?.Invoke(_rangeQuantifierService, rangeModel, numberRange) ??
                                        _isEqualToCheck(_rangeQuantifierService, rangeModel, numberRange.Start);
                    }
                default:
                    {
                        throw new InvalidOperationException("Unconfigured range quantifier option in this service!");
                    }
            }
        }
    }
}
