using System.Collections.Generic;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;
using FrannHammer.Services;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class CharacterAttributeTypeControllerTest : EffortBaseTest
    {
        private CharacterAttributeTypesController _controller;
        private IMetadataService _service;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _service = new MetadataService(Context);
            _controller = new CharacterAttributeTypesController(_service);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void ShouldGetCharacterAttributeType()
        {
            ExecuteAndReturnContent<CharacterAttributeTypeDto>(() => _controller.GetCharacterAttributeType(1));
        }

        [Test]
        public void ShouldGetAllCharacterAttributeTypes()
        {
            var characterAttributeTypes = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributeTypes())
                .ToList();

            CollectionAssert.AllItemsAreNotNull(characterAttributeTypes);
            CollectionAssert.AllItemsAreUnique(characterAttributeTypes);
            CollectionAssert.AllItemsAreInstancesOfType(characterAttributeTypes, typeof(CharacterAttributeTypeDto));
        }

        [Test]
        public void ShouldAddCharacterAttributeType()
        {
            var characterAttributeType = TestObjects.CharacterAttributeType();
            var result = ExecuteAndReturnCreatedAtRouteContent<CharacterAttributeTypeDto>(() => _controller.PostCharacterAttributeType(characterAttributeType));

            var latestCharacterAttributeType = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributeTypes()).ToList().Last();
            Assert.AreEqual(result, latestCharacterAttributeType);
        }

        [Test]
        public void ShouldUpdateCharacterAttributeType()
        {
            const string expectedName = "mewtwo";
            var characterAttributeType = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributeTypes()).First();

            //act
            if (characterAttributeType != null)
            {
                characterAttributeType.Name = expectedName;
                _controller.PutCharacterAttributeType(characterAttributeType.Id, characterAttributeType);
            }

            var updatedCharacter = ExecuteAndReturnContent<CharacterAttributeTypeDto>(() => _controller.GetCharacterAttributeType(characterAttributeType.Id));

            //assert
            Assert.That(updatedCharacter.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void ShouldDeleteCharacterAttributeType()
        {
            var characterAttributeType = ExecuteAndReturnContent<IEnumerable<dynamic>>(() => _controller.GetCharacterAttributeTypes()).ToList().First();
            var newCharacterAttributeType = ExecuteAndReturnCreatedAtRouteContent<CharacterAttributeTypeDto>(() => _controller.PostCharacterAttributeType(characterAttributeType));

            _controller.DeleteCharacterAttributeType(newCharacterAttributeType.Id);

            var characterAttributeTypesController = new CharacterAttributeTypesController(_service);
            ExecuteAndReturn<NotFoundResult>(() => characterAttributeTypesController.GetCharacterAttributeType(newCharacterAttributeType.Id));
        }
    }
}
