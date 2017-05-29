using System;
using System.Reflection;
using FrannHammer.Domain;
using FrannHammer.Domain.Contracts;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace FrannHammer.Api.Services.Tests
{
    [TestFixture]
    public class QueryMappingServiceTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullResourceQueryPassedInToMappingMethod()
        {
            var sut = new QueryMappingService(new Mock<IAttributeStrategy>().Object);
            Assert.Throws<ArgumentNullException>(() =>
            {
                sut.MapResourceQueryToDictionary(null, BindingFlags.Public | BindingFlags.Instance);
            });
        }

        [Test]
        public void ReturnedDictionaryDoesNotContainPropertyValuesThatWereNull()
        {
            var attributeStrategyMock = new Mock<IAttributeStrategy>();
            attributeStrategyMock.Setup(
                m =>
                    m.GetAttributeFromProperty<FriendlyNameAttribute>(It.IsAny<object>(),
                        It.IsAny<PropertyInfo>()))
                        .Returns(new FriendlyNameAttribute(Guid.NewGuid().ToString()));

            var sut = new QueryMappingService(attributeStrategyMock.Object);

            var resourceQueryStub = new TestMoveFilterResourceQuery();

            var results = sut.MapResourceQueryToDictionary(resourceQueryStub, BindingFlags.Public | BindingFlags.Instance);

            Assert.That(results.Count, Is.EqualTo(0), $"{nameof(results.Count)}");
        }

        [Test]
        public void MapsResourceQueryToDictionaryAsExpected()
        {
            //arrange
            var fixture = new Fixture();
            var attributeStrategyMock = new Mock<IAttributeStrategy>();
            attributeStrategyMock.Setup(
                m =>
                    m.GetAttributeFromProperty<FriendlyNameAttribute>(It.IsAny<object>(),
                        It.Is<PropertyInfo>(pi => pi.Name == nameof(IMoveFilterResourceQuery.Name))))
                        .Returns(new FriendlyNameAttribute(FriendlyNameMoveCommonConstants.OwnerName));

            var sut = new QueryMappingService(attributeStrategyMock.Object);

            var resourceQueryMock = new Mock<IMoveFilterResourceQuery>();
            resourceQueryMock.SetupAllProperties();
            resourceQueryMock.Object.Name = fixture.Create<string>();

            //act
            var results = sut.MapResourceQueryToDictionary(resourceQueryMock.Object, BindingFlags.Public | BindingFlags.Instance);

            //assert
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results.ContainsKey(FriendlyNameMoveCommonConstants.OwnerName));
        }

        class TestMoveFilterResourceQuery : IMoveFilterResourceQuery
        {
            public string Name { get; set; }
            public string MoveName { get; set; }
            public string MoveType { get; set; }
        }
    }
}
