using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests.PropertyParserTests
{
    [TestFixture]
    public class FirstActionableFrameParserTests
    {
        private FirstActionableFrameParser _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new FirstActionableFrameParser();
        }

        [Test]
        public void ParsingValidRawDataReturnsExpectedResultSingleHitbox()
        {
            const string testFrameNumber = "40";

            var results = _sut.Parse(testFrameNumber);

            Assert.That(results[MoveDataNameConstants.RawValueKey], Is.EqualTo(testFrameNumber), $"{nameof(results)}.{MoveDataNameConstants.RawValueKey}");
            Assert.That(results[MoveDataNameConstants.FrameKey], Is.EqualTo(testFrameNumber), $"{nameof(results)}.{MoveDataNameConstants.FrameKey}");
        }
    }
}