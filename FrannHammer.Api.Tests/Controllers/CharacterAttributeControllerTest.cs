using System;
using System.Threading;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models;
using NUnit.Framework;
using System.Linq;

namespace FrannHammer.Api.Tests.Controllers
{
    [TestFixture]
    public class CharacterAttributeControllerTest : EffortBaseTest
    {
        private CharacterAttributesController _controller;

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
            _controller = new CharacterAttributesController(Context);
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
            var charAttr = _controller.GetCharacterAttributes().First();
            Post(charAttr);
            Get(charAttr.Id);
        }

        [Test]
        public void ShouldGetAllCharacterAttributes()
        {
            var results = _controller.GetCharacterAttributes();
            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(CharacterAttributeDto));
        }

        [Test]
        public void ShouldAddCharacterAttribute()
        {
            var characterAttribute = _controller.GetCharacterAttributes().First();
            Post(characterAttribute);
        }

        [Test]
        public void ShouldUpdateCharacterAttribute()
        {
            const string expectedName = "mewtwo";
            var characterAttribute = _controller.GetCharacterAttributes().First();

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
            var characterAttribute = _controller.GetCharacterAttributes().First();
            Post(characterAttribute);

            _controller.DeleteCharacterAttribute(characterAttribute.Id);

            ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacterAttribute(characterAttribute.Id));
        }
    }
}
