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
        private const string IdRouteKey = "/{id}";
        private const string ThrowsRouteKey = "/throws";
        private const string MovesRouteKey = "/moves";
        private const string MovementsRouteKey = "/movements";
        private const string DetailsRouteKey = "/details";
        private const string CharacterAttributesRouteKey = "/characterattributes";
        private const string DetailedMovesRouteKey = "/detailedmoves";
        private const string SearchRouteKey = "/search";
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            Guard.VerifyObjectNotNull(characterService, nameof(characterService));
            _characterService = characterService;
        }

        [Route(CharactersRouteKey + IdRouteKey)]
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
        public override IHttpActionResult GetSingleByName(string name, [FromUri]string fields = "")
        {
            var character = _characterService.GetSingleByName(name, fields);
            return Result(character);
        }

        [Route(CharactersRouteKey + NameRouteKey + ThrowsRouteKey)]
        public IHttpActionResult GetAllThrowsForCharacterWhereName(string name, [FromUri]string fields = "")
        {
            var throws = _characterService.GetAllThrowsWhereCharacterNameIs(name, fields);
            return Result(throws);
        }

        [Route(CharactersRouteKey + IdRouteKey + ThrowsRouteKey)]
        public IHttpActionResult GetAllThrowsForCharacterWhereOwnerId(int id, [FromUri] string fields = "")
        {
            var throws = _characterService.GetAllThrowsWhereCharacterOwnerIdIs(id, fields);
            return Result(throws);
        }

        [Route(CharactersRouteKey + NameRouteKey + MovesRouteKey + SearchRouteKey)]
        public IHttpActionResult GetAllMovesForCharacterByNameWhereFilter([FromUri] MoveFilterResourceQuery query)
        {
            var moves = _characterService.GetAllMovesForCharacterByOwnerIdWhereFilter(query);
            return Result(moves);
        }

        [Route(CharactersRouteKey + IdRouteKey + MovesRouteKey + SearchRouteKey)]
        public IHttpActionResult GetAllMovesForCharacterByIdWhereFilter([FromUri] MoveFilterResourceQuery query)
        {
            var moves = _characterService.GetAllMovesForCharacterByNameWhereFilter(query);
            return Result(moves);
        }

        [Route(CharactersRouteKey + NameRouteKey + MovesRouteKey)]
        public IHttpActionResult GetAllMovesForCharacterWhereName(string name, [FromUri] string fields = "")
        {
            var moves = _characterService.GetAllMovesWhereCharacterNameIs(name, fields);
            return Result(moves);
        }

        [Route(CharactersRouteKey + IdRouteKey + MovesRouteKey)]
        public IHttpActionResult GetAllMovesForCharacterWhereOwnerId(int id, [FromUri] string fields = "")
        {
            var moves = _characterService.GetAllMovesWhereCharacterOwnerIdIs(id, fields);
            return Result(moves);
        }

        [Route(CharactersRouteKey + NameRouteKey + MovementsRouteKey)]
        public IHttpActionResult GetAllMovementsForCharacterWhereName(string name, [FromUri] string fields = "")
        {
            var movements = _characterService.GetAllMovementsWhereCharacterNameIs(name, fields);
            return Result(movements);
        }

        [Route(CharactersRouteKey + IdRouteKey + MovementsRouteKey)]
        public IHttpActionResult GetAllMovementsForCharacterWhereOwnerId(int id, [FromUri] string fields = "")
        {
            var movements = _characterService.GetAllMovementsWhereCharacterOwnerIdIs(id, fields);
            return Result(movements);
        }

        [Route(CharactersRouteKey + NameRouteKey + DetailsRouteKey)]
        public IHttpActionResult GetDetailsForCharacterByName(string name, [FromUri] string fields = "")
        {
            var dto = _characterService.GetCharacterDetailsWhereCharacterOwnerIs(name, fields);
            return Result(dto);
        }

        [Route(CharactersRouteKey + IdRouteKey + DetailsRouteKey)]
        public IHttpActionResult GetDetailsForCharacterByOwnerId(int id, [FromUri] string fields = "")
        {
            var dto = _characterService.GetCharacterDetailsWhereCharacterOwnerIdIs(id, fields);
            return Result(dto);
        }

        [Route(CharactersRouteKey + NameRouteKey + CharacterAttributesRouteKey)]
        public IHttpActionResult GetAttributesForCharacterByName(string name, [FromUri] string fields = "")
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterNameIs(name, fields);
            return Result(attributeRows);
        }

        [Route(CharactersRouteKey + IdRouteKey + CharacterAttributesRouteKey)]
        public IHttpActionResult GetAttributesForCharacterByOwnerId(int id, [FromUri] string fields = "")
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterOwnerIdIs(id, fields);
            return Result(attributeRows);
        }

        [Route(CharactersRouteKey + NameRouteKey + DetailedMovesRouteKey)]
        public IHttpActionResult GetDetailedMovesForCharacterByName(string name, [FromUri] string fields = "")
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterNameIs(name, fields);
            return Result(detailedMoves);
        }

        [Route(CharactersRouteKey + IdRouteKey + DetailedMovesRouteKey)]
        public IHttpActionResult GetDetailedMovesForCharacterByOwnerId(int id, [FromUri] string fields = "")
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterOwnerIdIs(id, fields);
            return Result(detailedMoves);
        }
    }
}