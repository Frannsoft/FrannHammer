using System.Linq;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    public class CharacterIntegrityTest : BaseDataIntegrityTest
    {
        [Test]
        [TestCase(1, "Bayonetta")]
        [TestCase(58, "Zerosuitsamus")]
        public void VerifyCharacterIdIsCorrect(int characterId, string characterName)
        {
            var character = Context.Characters.Find(characterId);
             
            Assert.That(character, Is.Not.Null);
            Assert.That(character.Name, Is.EqualTo(characterName));
            Assert.That(character.Id, Is.EqualTo(characterId));
        }

        [Test]
        public void VerifyCharacterCountIsExpectedAmount()
        {
            var characters = Context.Characters;

            Assert.That(characters.ToList().Count, Is.EqualTo(58));
        }
    }
}
