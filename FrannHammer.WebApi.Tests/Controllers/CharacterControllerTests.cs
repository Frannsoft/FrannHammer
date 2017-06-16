using System;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using AutoMapper;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.WebApi.Controllers;
using FrannHammer.WebApi.Models;
using Moq;
using NUnit.Framework;

namespace FrannHammer.WebApi.Tests.Controllers
{
    [TestFixture]
    public class CharacterControllerTests : BaseControllerTests
    {
        [Test]
        public void ThrowsArgumentNullExceptionForNullCharacterServiceInCtor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharacterController(null);
            });
        }

        [Test]
        public void GetASingleCharacterByNameContainsHypermediaLinksToMoves()
        {
            //automapper config
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Character, CharacterResource>();
            });

            //mock character service
            var mockCharacterService = new Mock<ICharacterService>();
            mockCharacterService.Setup(c => c.GetSingleByName(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Character
                {
                    Name = "mario",
                    OwnerId = 21,
                    ColorTheme = "#FFF"
                });

            //mock Url property since it requires full api context normally
            var mockUrlHelper = new Mock<UrlHelper>();
            mockUrlHelper.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>()))
                .Returns("/api/characters/name/mario/moves");

            var sut = new CharacterController(mockCharacterService.Object)
            {
                Url = mockUrlHelper.Object
            };

            var results = ((OkNegotiatedContentResult<CharacterResource>)sut.GetSingleByName("mario")).Content;

            Assert.That(results, Is.Not.Null, $"{nameof(results)} should not be null.");
            Assert.That(results.Links.Count(), Is.EqualTo(1), $"{nameof(results.Links)} count ");

            Assert.That(results.Links.Any(l => l.Rel.Equals("moves", StringComparison.CurrentCultureIgnoreCase)),
                $"{nameof(results.Links)} no link with rel equal to moves");
        }
    }
}
