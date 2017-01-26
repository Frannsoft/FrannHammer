using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FrannHammer.Api.Controllers;
using FrannHammer.Models.DTOs;
using FrannHammer.Services;
using FrannHammer.Services.MoveSearch;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    public class CharacterIntegrityTest : BaseDataIntegrityTest
    {
        [Test]
        [TestCase(1, "Bayonetta")]
        [TestCase(58, "Zerosuitsamus")]
        public void VerifyCharacterIdEndpointsAreCorrect(int characterId, string characterName)
        {
            var character = Context.Characters.Find(characterId);

            Assert.That(character, Is.Not.Null);
            Assert.That(character.Name, Is.EqualTo(characterName));
            Assert.That(character.Id, Is.EqualTo(characterId));
        }

        [Test]
        public void VerifyDisplayNameIsProperlyTrimmed()
        {
            var characters = Context.Characters.ToList();

            foreach (var character in characters)
            {
                string msg = $"displayname: {character.DisplayName} is not " +
                             $"equal to name: {character.Name}";
                if (character.Name.EndsWith("s"))
                {
                    Assert.That(character.DisplayName.EndsWith("s"), msg);
                    Assert.That(!character.DisplayName.EndsWith("'s"), msg);
                }
            }
        }

        [Test]
        [TestCase(1)]
        public void CharacterDetailsReturnsExpectedTypesOfDataById(int characterId)
        {
            var metadataService = new MetadataService(Context, new ResultValidationService());
            var controller = new CharactersController(metadataService);

            var response = controller.GetCharacterDetailsById(characterId) as OkNegotiatedContentResult<AggregateCharacterData>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(response.Content.CharacterAttributeData, Is.Not.Null);
            Assert.That(response.Content.Metadata, Is.Not.Null);
            Assert.That(response.Content.MovementData, Is.Not.Null);
        }

        [Test]
        [TestCase("drmario")]
        public void CharacterDetailsReturnsExpectedTypesOfDataByName(string characterName)
        {
            var metadataService = new MetadataService(Context, new ResultValidationService());
            var controller = new CharactersController(metadataService);

            var response = controller.GetCharacterDetailsByName(characterName) as OkNegotiatedContentResult<AggregateCharacterData>;

            Assert.That(response, Is.Not.Null);

            // ReSharper disable once PossibleNullReferenceException
            Assert.That(response.Content.CharacterAttributeData, Is.Not.Null);
            Assert.That(response.Content.Metadata, Is.Not.Null);
            Assert.That(response.Content.MovementData, Is.Not.Null);
        }

        [Test]
        [TestCase(23)]
        public void DetailsMoveDataIsAggregatedAsExpected(int characterId)
        {
            var metadataService = new MetadataService(Context, new ResultValidationService());
            var controller = new CharactersController(metadataService);

            var response = controller.GetDetailedMovesForCharacter(characterId) as OkNegotiatedContentResult<IEnumerable<DetailedMoveDto>>;

            Assert.That(response, Is.Not.Null);

            CollectionAssert.AllItemsAreUnique(response.Content);
            CollectionAssert.IsNotEmpty(response.Content);

            foreach (var detailedMove in response.Content)
            {
                // ReSharper disable once PossibleNullReferenceException
                Assert.That(detailedMove.MoveId, Is.GreaterThan(0));
                Assert.That(detailedMove.MoveName, Is.Not.Empty);
            }
        }

        //TODO:
        //test for invalid character id
        //test that properties return empty if nothing found
        //test that properties contain expected information
    }
}
