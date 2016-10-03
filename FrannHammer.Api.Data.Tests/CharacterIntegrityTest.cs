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
                if (character.Name.EndsWith("s"))
                {
                    Assert.That(character.DisplayName.EndsWith("s"));
                    Assert.That(!character.DisplayName.EndsWith("'s"));
                }
                else
                {
                    Assert.That(character.DisplayName.Last().Equals(character.Name.Last()));
                }
            }
        }
    }
}
