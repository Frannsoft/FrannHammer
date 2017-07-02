using System;
using FrannHammer.Domain.PropertyParsers;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests
{
    [TestFixture]
    public class PropertyParserAttributeTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullTypeInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new PropertyParserAttribute(null);
            });
        }

        [Test]
        public void ThrowsInvalidOperationExceptionIfTypeIsNotSubclassOfPropertyParserBaseClass()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new PropertyParserAttribute(typeof(Move));
            });
        }
    }
}
