using System.Linq;
using FrannHammer.WebScraper;
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

        
    }
}
