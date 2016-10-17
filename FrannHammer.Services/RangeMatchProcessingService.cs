using System;
using FrannHammer.Models;

namespace FrannHammer.Services
{
    public class RangeMatchProcessingService
    {
        private Func<RangeQuantifierService, RangeModel, int, int, bool> _isBetweenCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isGreaterThanCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isGreaterThanOrEqualToCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isLessThanCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isLessThanOrEqualToCheck;
        private Func<RangeQuantifierService, RangeModel, int, bool> _isEqualToCheck;

        private readonly RangeQuantifierService _rangeQuantifierService;

        public RangeMatchProcessingService()
        {
            _rangeQuantifierService = new RangeQuantifierService();
        }

        public void ConfigureIsBetweenCheck(Func<RangeQuantifierService, RangeModel, int, int, bool> isBetweenCheck) =>
            _isBetweenCheck = isBetweenCheck;

        public void ConfigureIsGreaterThanCheck(Func<RangeQuantifierService, RangeModel, int, bool> isGreaterThanCheck) =>
            _isGreaterThanCheck = isGreaterThanCheck;

        public void ConfigureIsLessThanCheck(Func<RangeQuantifierService, RangeModel, int, bool> isLessThanCheck) =>
            _isLessThanCheck = isLessThanCheck;

        public void ConfigureIsGreaterThanOrEqualToCheck(Func<RangeQuantifierService, RangeModel, int, bool> isGreaterThanOrEqualToCheck) =>
            _isGreaterThanOrEqualToCheck = isGreaterThanOrEqualToCheck;

        public void ConfigureIsLessThanOrEqualToCheck(Func<RangeQuantifierService, RangeModel, int, bool> isLessThanOrEqualToCheck) =>
          _isLessThanOrEqualToCheck = isLessThanOrEqualToCheck;

        public void ConfigureIsEqualToCheck(Func<RangeQuantifierService, RangeModel, int, bool> isEqualToCheck) =>
            _isEqualToCheck = isEqualToCheck;


        public bool Check(RangeModel rangeModel, int startValueFromDb, int endValueFromDb = -1)
        {
            switch (rangeModel.RangeQuantifier)
            {
                case RangeQuantifier.Between:
                    {
                        return _isBetweenCheck(_rangeQuantifierService, rangeModel, startValueFromDb, endValueFromDb);
                    }
                case RangeQuantifier.LessThan:
                    {
                        return _isLessThanCheck(_rangeQuantifierService, rangeModel, startValueFromDb);
                    }
                case RangeQuantifier.LessThanOrEqualTo:
                    {
                        return _isLessThanOrEqualToCheck(_rangeQuantifierService, rangeModel, startValueFromDb);
                    }
                case RangeQuantifier.GreaterThan:
                    {
                        return _isGreaterThanCheck(_rangeQuantifierService, rangeModel, startValueFromDb);
                    }
                case RangeQuantifier.GreaterThanOrEqualTo:
                    {
                        return _isGreaterThanOrEqualToCheck(_rangeQuantifierService, rangeModel, startValueFromDb);
                    }
                case RangeQuantifier.EqualTo:
                    {
                        return _isEqualToCheck(_rangeQuantifierService, rangeModel, startValueFromDb);
                    }
                default:
                    {
                        throw new InvalidOperationException("Unconfigured range quantifier option in this service!");
                    }
            }
        }
    }
}
