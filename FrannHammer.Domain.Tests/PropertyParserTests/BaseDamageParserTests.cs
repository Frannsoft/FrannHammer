using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;
using static FrannHammer.Domain.PropertyParsers.MoveDataNameConstants;

namespace FrannHammer.Domain.Tests.PropertyParserTests
{
    [TestFixture]
    public class BaseDamageParserTests
    {
        [Test]
        public void ParsingValidRawDataProducesExpectedResultForMultipleHitboxMoves()
        {
            var sut = new BaseDamageParser();

            var results = sut.Parse("16/14");

            Assert.That(results[Hitbox1Key], Is.EqualTo("16"), $"{nameof(results)}.{Hitbox1Key}");
            Assert.That(results[Hitbox2Key], Is.EqualTo("14"), $"{nameof(results)}.{Hitbox2Key}");
            Assert.That(results[RawValueKey], Is.EqualTo("16/14"), $"{nameof(results)}.{RawValueKey}");
        }

        [Test]
        public void ParsingValidRawDataProducesExpectedResultForSingleHitboxMoves()
        {
            var sut = new BaseDamageParser();

            var results = sut.Parse("1.7");

            Assert.That(results[Hitbox1Key], Is.EqualTo("1.7"), $"{nameof(results)}.{Hitbox1Key}");
        }

        [Test]
        public void AtteptToParseEmptyDataReturnsEmptyDictionary()
        {
            var sut = new BaseDamageParser();

            var results = sut.Parse(string.Empty);

            Assert.That(results, Is.Not.Null, $"{nameof(results)}");
            Assert.That(results.Keys.Count, Is.EqualTo(0), $"{nameof(results.Keys.Count)}");
        }
    }
}
