using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests.PropertyParserTests
{
    [TestFixture]
    public class AutoCancelParserTests
    {
        private AutocancelParser _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AutocancelParser();
        }

        [Test]
        public void ParsingValidRawDataProducesExpectedResultMultipleCancels()
        {
            var results = _sut.Parse("1-2, 26&gt;");

            Assert.That(results[MoveDataNameConstants.RawValueKey], Is.EqualTo("1-2, 26&gt;"), $"{nameof(results)}.{MoveDataNameConstants.RawValueKey}");
            Assert.That(results[MoveDataNameConstants.Cancel1Key], Is.EqualTo("1-2"), $"{nameof(results)}.{MoveDataNameConstants.Cancel1Key}");
            Assert.That(results[MoveDataNameConstants.Cancel2Key], Is.EqualTo("26>"), $"{nameof(results)}.{MoveDataNameConstants.Cancel2Key}");
        }

        [Test]
        public void ParsingValidRawDataProducesExpectedResultsForSingleAutocancel()
        {
            var results = _sut.Parse("28&gt;");

            Assert.That(results[MoveDataNameConstants.RawValueKey], Is.EqualTo("28&gt;"), $"{nameof(results)}.{MoveDataNameConstants.RawValueKey}");
            Assert.That(results[MoveDataNameConstants.Cancel1Key], Is.EqualTo("28>"), $"{nameof(results)}.{MoveDataNameConstants.Cancel1Key}");
        }
    }
}