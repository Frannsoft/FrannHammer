using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.Utility;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    [RoutePrefix(DefaultRoutePrefix)]
    public class CharacterController : BaseApiController
    {
        private const string CharactersRouteKey = "characters";
        private const string NameRouteKey = "/name/{name}";
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
        public IHttpActionResult GetByOwnerId(int id, [FromUri]string fields = "")
        {
            var character = _characterService.GetSingleByOwnerId(id, fields);
            return Result(character);
        }

        [Route(CharactersRouteKey)]
        public override IHttpActionResult GetAll([FromUri]string fields = "")
        {
            var characters = _characterService.GetAll(fields);
            return Result(characters);
        }

        [Route(CharactersRouteKey + NameRouteKey)]
        public override IHttpActionResult GetAllWhereName(string name, [FromUri]string fields = "")
        {
            var character = _characterService.GetSingleByName(name, fields);
            return Result(character);
        }

        [Route(CharactersRouteKey + NameRouteKey + "/throws")]
        public IHttpActionResult GetAllThrowsForCharacterWhereName(string name, [FromUri]string fields = "")
        {
            var throws = _moveService.GetAllThrowsWhereCharacterNameIs(name, fields);
            return Result(throws);
        }

        [Route(CharactersRouteKey + NameRouteKey + "/moves")]
        public IHttpActionResult GetAllMovesForCharacterWhereName([FromUri] MoveFilterResourceQuery query)
        {
            var moves = _moveService.GetAllWhere(query);
            return Result(moves);
        }

        [Route(CharactersRouteKey + NameRouteKey + "/movements")]
        public IHttpActionResult GetAllMovementsForCharacterWhereName(string name, [FromUri] string fields = "")
        {
            var movements = _characterService.GetAllMovementsWhereCharacterNameIs(name, fields);
            return Result(movements);
        }

        [Route(CharactersRouteKey + NameRouteKey + "/details")]
        public IHttpActionResult GetDetailsForCharacterByName(string name, [FromUri] string fields = "")
        {
            var dto = _characterService.GetCharacterDetails(name, fields);
            return Result(dto);
        }

        [Route(CharactersRouteKey + NameRouteKey + "/characterattributes")]
        public IHttpActionResult GetAttributesForCharacterByName(string name, [FromUri] string fields = "")
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterNameIs(name, fields);
            return Result(attributeRows);
        }

        [Route(CharactersRouteKey + NameRouteKey + "/detailedmoves")]
        public IHttpActionResult GetDetailedMovesForCharacterByName(string name, [FromUri] string fields = "")
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterNameIs(name, fields);
            return Result(detailedMoves);
        }
    }
}