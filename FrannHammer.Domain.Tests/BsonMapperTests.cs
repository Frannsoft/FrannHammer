using System.Linq;
using MongoDB.Bson.Serialization;
using NUnit.Framework;

namespace FrannHammer.Domain.Tests
{
    [TestFixture]
    public class BsonMapperTests
    {
        [Test]
        public void ExpectedPropertiesRegisterForType()
        {
            int initialCount = BsonClassMap.GetRegisteredClassMaps().Count();

            Assert.That(initialCount, Is.EqualTo(0), $"{nameof(initialCount)}");

            BsonMapper.RegisterTypeWithAutoMap<MongoModel>();

            var result = BsonClassMap.GetRegisteredClassMaps().ToList()[0];

            Assert.That(result.ClassType, Is.EqualTo(typeof(MongoModel)), "first result.");

            var memberMaps = result.DeclaredMemberMaps.ToList();

            Assert.That(memberMaps.Count, Is.EqualTo(2), $"{nameof(memberMaps.Count)}");
            Assert.That(memberMaps[0].ElementName, Is.EqualTo("Id"), "Id element name");
            Assert.That(memberMaps[1].ElementName, Is.EqualTo("name"), "Name element name");
        }
    }
}
