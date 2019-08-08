using System;
using System.Collections.Generic;
using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests
{
    [TestFixture]
    public class MovePropertyParserAttributeTests
    {
        [Test]
        public void CtorThrowForNullParserType()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new PropertyParserAttribute(null);
            });
        }

        [Test]
        public void PropertyParserTypeIsStoredAsExpectedInCtorWhenCorrectType()
        {
            var sut = new PropertyParserAttribute(typeof(TestPropertyParser));
            Assert.That(sut.ParserType, Is.EqualTo(typeof(TestPropertyParser)), $"{nameof(sut.ParserType)}");
        }

        [Test]
        public void CtorThrowsInvalidOperationExceptionForPropertyTypeNotDerivingFromPropertyParser()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new PropertyParserAttribute(typeof(int));
            });
        }
    }

    public class TestPropertyParser : PropertyParser
    {
        public override IDictionary<string, string> Parse(string rawData)
        {
            throw new NotImplementedException();
        }
    }
}
