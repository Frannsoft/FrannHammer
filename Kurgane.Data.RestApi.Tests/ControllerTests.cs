using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Kurogane.Data.RestApi.Controllers;
using Kurogane.Data.RestApi.Infrastructure;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;
using NUnit.Framework;
using Moq;

namespace Kurgane.Data.RestApi.Tests
{
    [TestFixture]
    public class ControllerTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
            RandomCharacters = SetupCharacters();
            CharacterStatRepository = SetupCharacterStatRepository();
            MovementStatRepository = SetupMovementStatRepository();
            MoveStatRepository = SetupMoveStatRepository();
            CharacterAttributeRepository = SetupCharacterAttributeRepository();
            UnitOfWork = new Mock<IUnitOfWork>().Object;
            CharacterStatService = new CharacterService(CharacterStatRepository, UnitOfWork);
            MoveService = new MoveService(MoveStatRepository, UnitOfWork);
            MovementService = new MovementStatService(MovementStatRepository, UnitOfWork);
            CharacterAttributeService = new CharacterAttributeService(CharacterAttributeRepository, UnitOfWork);
        }

        [Test]
        public void ControllerShouldReturnAllCharacters()
        {
            var characterController = new CharacterController(CharacterStatService, MovementService, MoveService,
                CharacterAttributeService);

            var result = characterController.GetCharacters() as OkNegotiatedContentResult<IEnumerable<CharacterStat>>;
            CollectionAssert.AreEqual(result.Content, RandomCharacters);
        }

        [Test]
        public void ControllerShouldReturnLastCharacter()
        {
            var characterController = new CharacterController(CharacterStatService, MovementService, MoveService,
                CharacterAttributeService);

            var result = characterController.GetCharacter(2) as OkNegotiatedContentResult<CharacterStat>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Content.Name, RandomCharacters.Last().Name);
        }

        [Test]
        public void ControllerShouldPutReturnBadRequest()
        {
            var characterController = new CharacterController(CharacterStatService, MovementService, MoveService,
                CharacterAttributeService)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("http://localhost/api/characters/1")
                }
            };

            var badresult = characterController.Put(-1, new CharacterStat() { Name = "bad" });
            Assert.That(badresult, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void ControllerShouldPutUpdateFirstCharacter()
        {
            var characterController = new CharacterController(CharacterStatService, MovementService, MoveService,
                CharacterAttributeService)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("http://localhost/api/characters/1")
                }
            };

            var updateResult = characterController.Put(1, new CharacterStat
            {
                Id = 1,
                ColorTheme = "#203983",
                Description = "desc",
                MainImageUrl = "miu",
                Name = "testname",
                //OwnerId = 23,
                Style = "teststyle",
                ThumbnailUrl = "tiu"
            });

            Assert.That(updateResult, Is.TypeOf<OkNegotiatedContentResult<CharacterStat>>());

            //StatusCodeResult statusCodeResult = updateResult as StatusCodeResult;

            //Assert.That(statusCodeResult.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(RandomCharacters.First().Name, Is.EqualTo("testname"));
        }

        [Test]
        public void ControllerShouldPostNewCharacter()
        {
            var character = new CharacterStat
            {
                Id = 3,
                ColorTheme = "#203983",
                Description = "desc",
                MainImageUrl = "miu",
                Name = "testname",
                //OwnerId = 23,
                Style = "teststyle",
                ThumbnailUrl = "tiu"
            };

            var characterController = new CharacterController(CharacterStatService, MovementService, MoveService,
               CharacterAttributeService)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost/api/characters")
                }
            };

            characterController.Configuration.MapHttpAttributeRoutes();
            characterController.Configuration.EnsureInitialized();
            characterController.RequestContext.RouteData = new HttpRouteData(
                new HttpRoute(), new HttpRouteValueDictionary { { "_characterController", "Characters" } });

            var result = characterController.Post(character) as CreatedAtRouteNegotiatedContentResult<CharacterStat>;

            Assert.That(result.RouteName, Is.EqualTo("DefaultApi"));
            Assert.That(result.Content.Id, Is.EqualTo(result.RouteValues["id"]));
            Assert.That(result.Content.Id, Is.EqualTo(RandomCharacters.Max(c => c.Id)));
        }

        [Test]
        public void ControllerShouldNotPostNewCharacter()
        {
            var character = new CharacterStat
            {
                Id = 3,
                ColorTheme = "#203983",
                Description = "desc",
                MainImageUrl = "miu",
                Name = "testname",
                //OwnerId = 23,
                Style = "teststyle",
                ThumbnailUrl = "tiu"
            };

            var characterController = new CharacterController(CharacterStatService, MovementService, MoveService,
               CharacterAttributeService)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost/api/characters")
                }
            };

            characterController.Configuration.MapHttpAttributeRoutes();
            characterController.Configuration.EnsureInitialized();
            characterController.RequestContext.RouteData = new HttpRouteData(
                new HttpRoute(), new HttpRouteValueDictionary { { "_characterController", "Characters" } });
            characterController.ModelState.AddModelError("Contents", "Contents is required field");

            var result = characterController.Post(character) as InvalidModelStateResult;

            Assert.That(result.ModelState.Count, Is.EqualTo(1));
            Assert.That(result.ModelState.IsValid, Is.EqualTo(false));
        }
    }


}
