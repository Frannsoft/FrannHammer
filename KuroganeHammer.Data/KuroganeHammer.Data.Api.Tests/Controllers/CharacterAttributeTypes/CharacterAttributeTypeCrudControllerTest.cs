using System;
using System.Linq;
using System.Web.Http.Results;
using KuroganeHammer.Data.Api.Models;
using NUnit.Framework;

namespace KuroganeHammer.Data.Api.Tests.Controllers.CharacterAttributeTypes
{
    [TestFixture]
    public class CharacterAttributeTypeCrudControllerTest : BaseControllerTest
    {
        [Test]
        public void ShouldGetCharacterAttributeType()
        {
            var characterAttributeType = TestObjects.CharacterAttributeType();
            CharacterAttributeTypesController.PostCharacterAttributeType(characterAttributeType);

            var result = CharacterAttributeTypesController.GetCharacterAttributeType(characterAttributeType.Id) as OkNegotiatedContentResult<CharacterAttributeType>;

            Assert.That(result?.Content, Is.Not.Null);
        }

        [Test]
        public void ShouldGetAllCharacterAttributeTypes()
        {
            var characterAttributeType = TestObjects.CharacterAttributeType();
            CharacterAttributeTypesController.PostCharacterAttributeType(characterAttributeType);

            var results = CharacterAttributeTypesController.GetCharacterAttributeTypes();

            Assert.That(results.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldAddCharacterAttributeType()
        {
            var characterAttributeType = TestObjects.CharacterAttributeType();
            var result = CharacterAttributeTypesController.PostCharacterAttributeType(characterAttributeType) as CreatedAtRouteNegotiatedContentResult<CharacterAttributeType>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Content, Is.Not.Null);
            Assert.AreEqual(characterAttributeType, result?.Content);
        }

        [Test]
        public void ShouldUpdateCharacterAttributeType()
        {
            const string expectedName = "mewtwo";
            var characterAttributeType = TestObjects.CharacterAttributeType();

            var dateTime = DateTime.Now;

            //arrange
            var returnedCharacter =
                CharacterAttributeTypesController.PostCharacterAttributeType(characterAttributeType) as CreatedAtRouteNegotiatedContentResult<CharacterAttributeType>;
            //act
            if (returnedCharacter != null)
            {
                returnedCharacter.Content.Name = expectedName;
                CharacterAttributeTypesController.PutCharacterAttributeType(returnedCharacter.Content.Id, returnedCharacter.Content);
            }

            var updatedCharacter = CharacterAttributeTypesController.GetCharacterAttributeType(characterAttributeType.Id) as OkNegotiatedContentResult<CharacterAttributeType>;

            //assert
            Assert.That(updatedCharacter?.Content.Name, Is.EqualTo(expectedName));
            Assert.That(updatedCharacter?.Content.LastModified, Is.GreaterThan(dateTime));
        }

        [Test]
        public void ShouldDeleteCharacterAttributeType()
        {
            var characterAttribute = TestObjects.CharacterAttributeType();
            CharacterAttributeTypesController.PostCharacterAttributeType(characterAttribute);

            CharacterAttributeTypesController.DeleteCharacterAttributeType(characterAttribute.Id);

            var result = CharacterAttributesController.GetCharacterAttribute(characterAttribute.Id) as NotFoundResult;
            Assert.That(result, Is.Not.Null);
        }
    }
}
