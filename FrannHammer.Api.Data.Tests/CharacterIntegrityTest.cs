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
    }
}
