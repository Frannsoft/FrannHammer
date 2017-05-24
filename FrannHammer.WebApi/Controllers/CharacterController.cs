using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Utility;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    [RoutePrefix(DefaultRoutePrefix)]
    public class CharacterController : BaseApiController
    {
        private const string CharactersRouteKey = "characters";
        private readonly ICharacterService _characterService;
        private readonly IMoveService _moveService;

        public CharacterController(ICharacterService characterService, IMoveService moveService)
        {
            Guard.VerifyObjectNotNull(characterService, nameof(characterService));
            Guard.VerifyObjectNotNull(moveService, nameof(moveService));

            _characterService = characterService;
            _moveService = moveService;
        }

        [Route(CharactersRouteKey + "/{id}")]
        public override IHttpActionResult GetById(string id, [FromUri]string fields = "")
        {
            var character = _characterService.GetSingleById(id, fields);
            return Result(character);
        }

        [Route(CharactersRouteKey)]
        public override IHttpActionResult GetAll([FromUri]string fields = "")
        {
            var characters = _characterService.GetAll(fields);
            return Result(characters);
        }

        [Route(CharactersRouteKey + "/name/{name}")]
        public override IHttpActionResult GetAllWhereName(string name, string fields = "")
        {
            var character = _characterService.GetAllWhereName(name, fields);
            return Result(character);
        }

        [Route(CharactersRouteKey + "/name/{name}/throws")]
        public IHttpActionResult GetAllThrowsForCharacterWhereName(string name, string fields = "")
        {
            var throws = _moveService.GetAllThrowsWhereCharacterNameIs(name, fields);
            return Result(throws);
        }
    }
}