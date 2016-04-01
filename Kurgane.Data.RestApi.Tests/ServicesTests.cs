using System;
using System.Collections.Generic;
using System.Linq;
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;
using Moq;
using NUnit.Framework;

namespace Kurgane.Data.RestApi.Tests
{

    [TestFixture]
    public class ServicesTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            RandomCharacters = SetupCharacters();
            CharacterStatRepository = SetupCharacterStatRepository();
            UnitOfWork = new Mock<IUnitOfWork>().Object;
            CharacterStatService = new CharacterService(CharacterStatRepository, UnitOfWork);
        }

        [Test]
        public void ServiceShouldReturnAllCharacters()
        {
            var characters = CharacterStatService.GetCharacters();
            Assert.That(characters, Is.EqualTo(RandomCharacters));
        }

        [Test]
        public void ServiceShouldReturnRightCharacter()
        {
            var testCharacter = CharacterStatService.GetCharacter(1);
            Assert.That(testCharacter,
                Is.EqualTo(RandomCharacters.Find(c => c.Name.Equals("test character"))));
        }

        [Test]
        public void ServiceShouldAddNewCharacter()
        {
            var newCharacter = new CharacterStat()
            {
                ColorTheme = "#2323512",
                Description = "new description",
                MainImageUrl = "http://woeihf.com/i.png",
                Name = "new character name",
                //OwnerId = 32,
                Style = "new character style",
                ThumbnailUrl = "http://woeihfwef.com/ii.png"
            };

            int maxCharacterIdBeforeAdd = RandomCharacters.Max(c => c.Id);
            CharacterStatService.CreateCharacter(newCharacter);

            Assert.That(newCharacter, Is.EqualTo(RandomCharacters.Last()));
            Assert.That(maxCharacterIdBeforeAdd + 1, Is.EqualTo(RandomCharacters.Last().Id));
        }

        [Test]
        public void ServiceShouldUpdateCharacter()
        {
            var firstCharacter = RandomCharacters.First();

            firstCharacter.Name = "updated the name";
            firstCharacter.Style = "updated the style";
            CharacterStatService.UpdateCharacter(firstCharacter);

            Assert.That(firstCharacter.Name, Is.EqualTo("updated the name"));
            Assert.That(firstCharacter.Style, Is.EqualTo("updated the style"));
            Assert.That(firstCharacter.LastModified, Is.Not.EqualTo(DateTime.MinValue));
            Assert.That(firstCharacter.Id, Is.EqualTo(1)); //hasn't changed
        }

        [Test]
        public void ServiceShouldDeleteCharacter()
        {
            var maxId = RandomCharacters.Max(c => c.Id);
            var lastCharacter = RandomCharacters.Last();

            //remove last character
            CharacterStatService.DeleteCharacter(lastCharacter);

            Assert.That(maxId, Is.GreaterThan(RandomCharacters.Max(c => c.Id)));
        }
    }
}
