using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests.PropertyParserTests
{
    [TestFixture]
    public class LandingLagParserTests
    {
        private LandingLagParser _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new LandingLagParser();
        }

        [Test]
        public void ParsingValidRawDataReturnsExpectedResultSingleHitbox()
        {
            const string testLandingLagValue = "28";

            var results = _sut.Parse(testLandingLagValue);

            Assert.That(results[MoveDataNameConstants.RawValueKey], Is.EqualTo(testLandingLagValue), $"{nameof(results)}.{MoveDataNameConstants.RawValueKey}");
            Assert.That(results[MoveDataNameConstants.FramesKey], Is.EqualTo(testLandingLagValue), $"{nameof(results)}.{MoveDataNameConstants.FrameKey}");
        }
    }
}