using System.Collections.Generic;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;
using FrannHammer.Services;
using Newtonsoft.Json;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class CharacterAttributeControllerTest : EffortBaseTest
    {
        private CharacterAttributesController _controller;
        private IMetadataService _service;

        private CharacterAttributeDto Post(CharacterAttributeDto characterAttribute)
        {
            return ExecuteAndReturnCreatedAtRouteContent<CharacterAttributeDto>(
                () => _controller.PostCharacterAttribute(characterAttribute));
        }

        private CharacterAttributeDto Get(int id)
        {
            return ExecuteAndReturnContent<CharacterAttributeDto>(
                () => _controller.GetCharacterAttribute(id));
        }

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _service = new MetadataService(Context);
            _controller = new CharacterAttributesController(_service);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void ShouldGetCharacterAttribute()
        {
            var charAttr = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributes()).First();
            Post(charAttr);
            Get(charAttr.Id);
        }

        [Test]
        public void ShouldGetAllCharacterAttributes()
        {
            var results = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributes())
                .ToList();

            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
        }

        [Test]
        public void ShouldAddCharacterAttribute()
        {
            var first = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributes()).First();

            var json = JsonConvert.SerializeObject(first);
            var characterAttribute = JsonConvert.DeserializeObject<CharacterAttributeDto>(json);

            var result = ExecuteAndReturnCreatedAtRouteContent<CharacterAttributeDto>(() => _controller.PostCharacterAttribute(characterAttribute));

            var latest =
                ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributes())
                    .ToList()
                    .Last();

            Assert.AreEqual(result.CharacterAttributeTypeId, latest.CharacterAttributeTypeId);
            Assert.AreEqual(result.Id, latest.Id);
            Assert.AreEqual(result.Name, latest.Name);
            Assert.AreEqual(result.OwnerId, latest.OwnerId);
            Assert.AreEqual(result.Rank, latest.Rank);
            Assert.AreEqual(result.SmashAttributeTypeId, latest.SmashAttributeTypeId);
            Assert.AreEqual(result.Value, latest.Value);
        }

        [Test]
        public void ShouldUpdateCharacterAttribute()
        {
            const string expectedName = "mewtwo";
            var result = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributes()).First();

            var json = JsonConvert.SerializeObject(result);
            var characterAttribute = JsonConvert.DeserializeObject<CharacterAttributeDto>(json);

            //act
            if (characterAttribute != null)
            {
                characterAttribute.Name = expectedName;
                _controller.PutCharacterAttribute(characterAttribute.Id, characterAttribute);
            }

            var updatedCharacter = Get(characterAttribute.Id);

            //assert
            Assert.That(updatedCharacter?.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void ShouldDeleteCharacterAttribute()
        {
            var characterAttribute = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributes()).First();
            Post(characterAttribute);

            _controller.DeleteCharacterAttribute(characterAttribute.Id);

            ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacterAttribute(characterAttribute.Id));
        }
    }
}
