using FrannHammer.Models;

namespace FrannHammer.Services.MoveSearch
{
    public class RangeQuantifierService
    {
        public bool IsBetween(int valueUnderTest, NumberRange boundaries)
        {
            if (boundaries.End.HasValue)
            {
                return valueUnderTest > boundaries.Start && valueUnderTest < boundaries.End.Value;
            }
            else
            {
                return valueUnderTest > boundaries.Start;
            }
        }

        public bool IsBetween(int lowValueUnderTest, int highValueUnderTest, NumberRange boundaries)
        {
            if (boundaries.End.HasValue)
            {
                return lowValueUnderTest > boundaries.Start && highValueUnderTest < boundaries.End.Value ||
                       lowValueUnderTest < boundaries.Start && highValueUnderTest > boundaries.Start;
            }
            else
            {
                return lowValueUnderTest < boundaries.Start &&
                        highValueUnderTest > boundaries.Start &&
                        highValueUnderTest > lowValueUnderTest;
            }
        }

        public bool IsGreaterThan(int valueUnderTest, int boundary) =>
            valueUnderTest > boundary;

        public bool IsGreaterThanOrEqualTo(int valueUnderTest, int boundary) =>
            valueUnderTest >= boundary;

        public bool IsLessThan(int valueUnderTest, int boundary) =>
            valueUnderTest < boundary;

        public bool IsLessThanOrEqualTo(int valueUnderTest, int boundary) =>
            valueUnderTest <= boundary;

        public bool IsEqualTo(int actualValue, int expectedValue) =>
            expectedValue == actualValue;
    }
}
