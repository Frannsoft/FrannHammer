namespace FrannHammer.Services
{
    public class RangeQuantifierService
    {
        public bool IsBetween(int valueUnderTest, int lowBoundary, int highBoundary) =>
             valueUnderTest > lowBoundary && valueUnderTest < highBoundary;

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
