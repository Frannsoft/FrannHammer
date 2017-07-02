using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.Tests.PropertyParserTests
{
    [TestFixture]
    public class HitboxParserTests
    {
        private HitboxParser _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new HitboxParser();
        }

        [Test]
        public void ParsingValidRawDataProducesExpectedResultForMultipleHitboxMoves()
        {
            var results = _sut.Parse("16/14");

            Assert.That(results[Hitbox1Key], Is.EqualTo("16"), $"{nameof(results)}.{Hitbox1Key}");
            Assert.That(results[Hitbox2Key], Is.EqualTo("14"), $"{nameof(results)}.{Hitbox2Key}");
            Assert.That(results[RawValueKey], Is.EqualTo("16/14"), $"{nameof(results)}.{RawValueKey}");
        }

        [Test]
        public void ParsingValidRawDataProducesExpectedResultForSingleHitboxMoves()
        {
            const string testValue = "1.7";
            var results = _sut.Parse(testValue);

            Assert.That(results[RawValueKey], Is.EqualTo(testValue), $"{nameof(results)}.{RawValueKey}");
            Assert.That(results[Hitbox1Key], Is.EqualTo(testValue), $"{nameof(results)}.{Hitbox1Key}");
        }
    }
}
