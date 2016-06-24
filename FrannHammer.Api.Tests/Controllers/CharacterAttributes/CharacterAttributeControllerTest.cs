using System;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Core.Models;
using NUnit.Framework;

namespace FrannHammer.Api.Tests.Controllers.CharacterAttributes
{
    [TestFixture]
    public class CharacterAttributeControllerTest : EffortBaseTest
    {
        private CharacterAttributesController _controller;

        private CharacterAttribute Post(CharacterAttribute characterAttribute)
        {
            return ExecuteAndReturnCreatedAtRouteContent<CharacterAttribute>(
                () => _controller.PostCharacterAttribute(characterAttribute));
        }

        private CharacterAttribute Get(int id)
        {
            return ExecuteAndReturnContent<CharacterAttribute>(
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
            var charAttr = TestObjects.CharacterAttribute();
            Post(charAttr);
            Get(charAttr.Id);
        }

        [Test]
        public void ShouldGetAllCharacterAttributes()
        {
            var results = _controller.GetCharacterAttributes();
            CollectionAssert.AllItemsAreNotNull(results);
            CollectionAssert.AllItemsAreUnique(results);
            CollectionAssert.AllItemsAreInstancesOfType(results, typeof(CharacterAttribute));
        }

        [Test]
        public void ShouldAddCharacterAttribute()
        {
            var characterAttribute = TestObjects.CharacterAttribute();
            Post(characterAttribute);
        }

        [Test]
        public void ShouldUpdateCharacterAttribute()
        {
            const string expectedName = "mewtwo";
            var characterAttribute = TestObjects.CharacterAttribute();

            var dateTime = DateTime.Now;

            //arrange
            var returnedCharacter = Post(characterAttribute);
            //act
            if (returnedCharacter != null)
            {
                returnedCharacter.Name = expectedName;
                _controller.PutCharacterAttribute(returnedCharacter.Id, returnedCharacter);
            }

            var updatedCharacter = Get(characterAttribute.Id);

            //assert
            Assert.That(updatedCharacter?.Name, Is.EqualTo(expectedName));
            Assert.That(updatedCharacter?.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteCharacterAttribute()
        {
            var characterAttribute = TestObjects.CharacterAttribute();
            Post(characterAttribute);

            _controller.DeleteCharacterAttribute(characterAttribute.Id);

            ExecuteAndReturn<NotFoundResult>(() => _controller.GetCharacterAttribute(characterAttribute.Id));
        }
    }
}
