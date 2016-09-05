using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FrannHammer.Models;
using FrannHammer.WebScraper;
using NUnit.Framework;

namespace FrannHammer.Api.Data.Tests
{
    [TestFixture]
    public class CharacterDbToKhIntegrityTest : BaseDataIntegrityTest
    {
        private List<Thumbnail> _thumbnailUrls;
        private IDbSet<Character> _characters;

        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
            _thumbnailUrls = new HomePage("http://kuroganehammer.com/Smash4/").GetThumbnailData();
            _characters = Context.Characters;
        }

        [Test]
        public void CharacterDbDataIsEqualToKhPageData()
        {
            foreach (var characterFromDb in _characters)
            {
                var characterFromKhPage = new WebCharacter(characterFromDb);
                AssertCharacterDataMatches(characterFromDb, characterFromKhPage);
            }
        }

        private void AssertCharacterDataMatches(BaseCharacterModel characterFromDb,
            WebCharacter webCharacterFromKhPage)
        {
            Assert.That(characterFromDb.Name, Is.Not.EqualTo(string.Empty));
            Assert.That(characterFromDb.Name, Is.Not.Null);

            Assert.That(characterFromDb.Name, Is.EqualTo(webCharacterFromKhPage.Name), 
                $"{characterFromDb.Name} does not match Kh page value for character {webCharacterFromKhPage.Name}");

            Assert.That(characterFromDb.MainImageUrl, Is.EqualTo(webCharacterFromKhPage.MainImageUrl),
                $"{characterFromDb.Name} MainImageUrl does not match Kh page value for character {webCharacterFromKhPage.Name}");

            Assert.That(characterFromDb.ColorTheme, Is.EqualTo(webCharacterFromKhPage.ColorHex),
                $"{characterFromDb.Name} ColorTheme does not match Kh page value for character {webCharacterFromKhPage.Name}");

            Assert.That(characterFromDb.ThumbnailUrl,
                Is.EqualTo(_thumbnailUrls.First(t => t.Key.Equals(webCharacterFromKhPage.Name.ToUpper())).Url),
                $"{characterFromDb.Name} ThumbnailUrl does not match Kh page value for character {webCharacterFromKhPage.Name}");
        }
    }
}
