using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests.PropertyParserTests
{
    [TestFixture]
    public class BaseKnockbackParserTests
    {
        private BaseKnockbackParser _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new BaseKnockbackParser();
        }

        [Test]
        public void ParsingValidRawDataProducesExpectedResultMultipleHitboxes()
        {
            var results = _sut.Parse("B: 35/15");

            Assert.That(results[MoveDataNameConstants.RawValueKey], Is.EqualTo("35/15"), $"{nameof(results)}.{MoveDataNameConstants.RawValueKey}");
            Assert.That(results[MoveDataNameConstants.Hitbox1Key], Is.EqualTo("35"), $"{nameof(results)}.{MoveDataNameConstants.Hitbox1Key}");
            Assert.That(results[MoveDataNameConstants.Hitbox2Key], Is.EqualTo("15"), $"{nameof(results)}.{MoveDataNameConstants.Hitbox2Key}");
        }

        [Test]
        public void AbleToSeparateBaseFromSetKnockbackDataWhenParsing()
        {
            var results = _sut.Parse("W: 30/B: 30/30");

            Assert.That(results[MoveDataNameConstants.RawValueKey], Is.EqualTo("30/30"), $"{nameof(results)}.{MoveDataNameConstants.RawValueKey}");
            Assert.That(results[MoveDataNameConstants.Hitbox1Key], Is.EqualTo("30"), $"{nameof(results)}.{MoveDataNameConstants.Hitbox1Key}");
            Assert.That(results[MoveDataNameConstants.Hitbox2Key], Is.EqualTo("30"), $"{nameof(results)}.{MoveDataNameConstants.Hitbox2Key}");
        }

        [Test]
        public void ParsingValidRawDataProducesExpectedResultsForSingleHitbox()
        {
            const string testValue = "60";

            var results = _sut.Parse(testValue);

            Assert.That(results[MoveDataNameConstants.RawValueKey], Is.EqualTo(testValue), $"{nameof(results)}.{MoveDataNameConstants.RawValueKey}");
            Assert.That(results[MoveDataNameConstants.Hitbox1Key], Is.EqualTo(testValue), $"{nameof(results)}.{MoveDataNameConstants.Hitbox1Key}");
        }
    }
}