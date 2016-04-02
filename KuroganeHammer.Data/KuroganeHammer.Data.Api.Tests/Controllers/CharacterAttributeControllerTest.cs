using System;
using System.Linq;
using System.Web.Http.Results;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Controllers
{
    [TestFixture]
    public class CharacterAttributeControllerTest : BaseControllerTest
    {
        [Test]
        public void ShouldGetCharacterAttribute()
        {
            var characterAttribute = TestObjects.CharacterAttribute();
            CharacterAttributesController.PostCharacterAttribute(characterAttribute);

            var result = CharacterAttributesController.GetCharacterAttribute(characterAttribute.Id) as OkNegotiatedContentResult<CharacterAttribute>;

            Assert.That(result?.Content, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllCharacterAttributes()
        {
            var newCharacter = TestObjects.CharacterAttribute();
            CharacterAttributesController.PostCharacterAttribute(newCharacter);

            var results = CharacterAttributesController.GetCharacterAttributes();

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldAddCharacterAttribute()
        {
            var characterAttribute = TestObjects.CharacterAttribute();
            var result = CharacterAttributesController.PostCharacterAttribute(characterAttribute) as CreatedAtRouteNegotiatedContentResult<CharacterAttribute>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Content, Is.Not.Null);
            Assert.AreEqual(characterAttribute, result?.Content);
        }

        [Test]
        public void ShouldUpdateCharacterAttribute()
        {
            const string expectedName = "mewtwo";
            var characterAttribute = TestObjects.CharacterAttribute();

            var dateTime = DateTime.Now;

            //arrange
            var returnedCharacter =
                CharacterAttributesController.PostCharacterAttribute(characterAttribute) as CreatedAtRouteNegotiatedContentResult<CharacterAttribute>;
            //act
            if (returnedCharacter != null)
            {
                returnedCharacter.Content.Name = expectedName;
                CharacterAttributesController.PutCharacterAttribute(returnedCharacter.Content.Id, returnedCharacter.Content);
            }

            var updatedCharacter = CharacterAttributesController.GetCharacterAttribute(characterAttribute.Id) as OkNegotiatedContentResult<CharacterAttribute>;

            //assert
            Assert.That(updatedCharacter?.Content.Name, Is.EqualTo(expectedName));
            Assert.That(updatedCharacter?.Content.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteCharacterAttribute()
        {
            var characterAttribute = TestObjects.CharacterAttribute();
            CharacterAttributesController.PostCharacterAttribute(characterAttribute);

            CharacterAttributesController.DeleteCharacterAttribute(characterAttribute.Id);

            var result = CharacterAttributesController.GetCharacterAttribute(characterAttribute.Id) as NotFoundResult;
            Assert.That(result, Is.Not.Null);
        }
    }
}
