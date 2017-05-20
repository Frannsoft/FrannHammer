using System;
using System.Collections.Generic;
using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests.PropertyParserTests
{
    [TestFixture]
    public class GeneralParserTests
    {
        private static IEnumerable<Tuple<int, PropertyParser>> ParserTypes()
        {
            yield return Tuple.Create<int, PropertyParser>(3, new AutocancelParser());
            yield return Tuple.Create<int, PropertyParser>(6, new HitboxParser());
            yield return Tuple.Create<int, PropertyParser>(6, new BaseKnockbackParser());
            yield return Tuple.Create<int, PropertyParser>(6, new SetKnockbackParser());
            yield return Tuple.Create<int, PropertyParser>(2, new FirstActionableFrameParser());
            yield return Tuple.Create<int, PropertyParser>(2, new LandingLagParser());
        }
        [Test]
        [TestCaseSource(nameof(ParserTypes))]
        public void AttemptToParseEmptyDataReturnsDictionaryWithExpectedNumberOfKeys(Tuple<int, PropertyParser> testData)
        {
            var sut = testData.Item2;
            var expectedNumberOfKeys = testData.Item1;

            var results = sut.Parse(string.Empty);

            Assert.That(results, Is.Not.Null, $"{nameof(results)}");
            Assert.That(results.Keys.Count, Is.EqualTo(expectedNumberOfKeys), $"{nameof(results.Keys.Count)}");
        }

        [Test]
        [TestCaseSource(nameof(ParserTypes))]
        public void EmptyFirstActionableFramePropertyReturnsDictionaryWithExpectedNumberOfKeys(Tuple<int, PropertyParser> testData)
        {
            var sut = testData.Item2;
            var expectedNumberOfKeys = testData.Item1;

            var results = sut.Parse("-");

            Assert.That(results, Is.Not.Null, $"{nameof(results)}");
            Assert.That(results.Keys.Count, Is.EqualTo(expectedNumberOfKeys), $"{nameof(results.Keys.Count)}");
        }
    }
}