using System.Web.Http.Results;
using NUnit.Framework;
using FrannHammer.Api.Controllers;
using FrannHammer.Core.Models;

namespace FrannHammer.Api.Tests.Controllers.Characters
{
    [TestFixture]
    public class CharacterTests : EffortBaseTest
    {
        private CharactersController _controller;

        [TestFixtureSetUp]
        public override void TestFixtureSetUp()
        {
            base.TestFixtureSetUp();
            _controller = new CharactersController(Context);
        }

        [TestFixtureTearDown]
        public override void TestFixtureTearDown()
        {
            _controller.Dispose();
            base.TestFixtureTearDown();
        }

        [Test]
        public void CanGetCharacterById()
        {
            var character = _controller.GetCharacter(1) as OkNegotiatedContentResult<Character>;
            Assert.That(character, Is.Not.Null);
        }

        [Test]
        public void CanGetCharacterByName()
        {
            const string expectedName = "Pikachu";
            var character = _controller.GetCharacterByName(expectedName) as OkNegotiatedContentResult<Character>;

            Assert.That(character, Is.Not.Null);
            Assert.That(character?.Content.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void NotFoundResultWhenNoCharacterFoundById()
        {
            var result = _controller.GetCharacter(0) as NotFoundResult;

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void NotFoundResultWhenNoCharacterFoundByName()
        {
            const string expectedName = "dummyvalue";
            var result = _controller.GetCharacterByName(expectedName) as NotFoundResult;

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void BadRequestReturned_WhenEmptyNameForFoundByName()
        {
            var result = _controller.GetCharacterByName(string.Empty) as BadRequestErrorMessageResult;
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void BadRequestReturned_WhenNullNameForFoundByName()
        {
            var result = _controller.GetCharacterByName(null) as BadRequestErrorMessageResult;
            Assert.That(result, Is.Not.Null);
        }
    }
}
