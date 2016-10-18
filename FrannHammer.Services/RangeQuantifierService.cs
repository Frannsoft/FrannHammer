namespace FrannHammer.Services
{
    public class RangeQuantifierService
    {
        public bool IsBetween(int valueUnderTest, int lowBoundary, int highBoundary)
        {
            if (highBoundary > -1)
            {
                return valueUnderTest > lowBoundary && valueUnderTest < highBoundary;
            }
            else
            {
                return valueUnderTest > lowBoundary;
            }
        }

        public bool IsBetween(int lowValueUnderTest, int lowBoundary, int highValueUnderTest, int highBoundary)
        {
            if (highBoundary > -1)
            {
                return (lowValueUnderTest > lowBoundary && highValueUnderTest < highBoundary) ||
                       (lowValueUnderTest < lowBoundary && highValueUnderTest > lowBoundary);
            }
            else
            {
                return lowValueUnderTest > lowBoundary && 
                       highValueUnderTest > lowBoundary && 
                       highValueUnderTest >= lowValueUnderTest;
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
