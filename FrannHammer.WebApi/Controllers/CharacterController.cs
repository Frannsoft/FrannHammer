using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain;
using FrannHammer.Utility;
using FrannHammer.WebApi.ActionFilterAttributes;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    //TODO - support hypermedia links for the rest of these routes
    //add support for many characters in a response instead of just one
    //add support for creating a characterattribute Link
    //make sure Specs check for this data

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

        [SingleCharacterResourceHalSupport]
        [Route(CharactersRouteKey + IdRouteKey, Name = nameof(GetByOwnerId))]
        public IHttpActionResult GetByOwnerId(int id, [FromUri]string fields = "")
        {
            var character = _characterService.GetSingleByOwnerId(id, fields);
            return Result(character);
        }

        [SingleCharacterResourceHalSupport]
        [Route(CharactersRouteKey + NameRouteKey, Name = nameof(GetSingleByName))]
        public override IHttpActionResult GetSingleByName(string name, [FromUri]string fields = "")
        {
            var character = _characterService.GetSingleByName(name, fields);
            return Result(character);
        }

        [ManyCharacterResourceHalSupport]
        [Route(CharactersRouteKey, Name = nameof(GetAll))]
        public override IHttpActionResult GetAll([FromUri]string fields = "")
        {
            var characters = _characterService.GetAll(fields);
            return Result(characters);
        }

        [Route(CharactersRouteKey + NameRouteKey + ThrowsRouteKey, Name = nameof(GetAllThrowsForCharacterWhereName))]
        public IHttpActionResult GetAllThrowsForCharacterWhereName(string name, [FromUri]string fields = "")
        {
            var throws = _characterService.GetAllThrowsWhereCharacterNameIs(name, fields);
            return Result(throws);
        }

        [Route(CharactersRouteKey + IdRouteKey + ThrowsRouteKey, Name = nameof(GetAllThrowsForCharacterWhereOwnerId))]
        public IHttpActionResult GetAllThrowsForCharacterWhereOwnerId(int id, [FromUri] string fields = "")
        {
            var throws = _characterService.GetAllThrowsWhereCharacterOwnerIdIs(id, fields);
            return Result(throws);
        }

        [Route(CharactersRouteKey + NameRouteKey + MovesRouteKey + SearchRouteKey, Name = nameof(GetAllMovesForCharacterByNameWhereFilter))]
        public IHttpActionResult GetAllMovesForCharacterByNameWhereFilter([FromUri] MoveFilterResourceQuery query)
        {
            var moves = _characterService.GetAllMovesForCharacterByOwnerIdFilteredBy(query);
            return Result(moves);
        }

        [Route(CharactersRouteKey + IdRouteKey + MovesRouteKey + SearchRouteKey, Name = nameof(GetAllMovesForCharacterByIdWhereFilter))]
        public IHttpActionResult GetAllMovesForCharacterByIdWhereFilter([FromUri] MoveFilterResourceQuery query)
        {
            var moves = _characterService.GetAllMovesForCharacterByNameFilteredBy(query);
            return Result(moves);
        }

        [Route(CharactersRouteKey + NameRouteKey + MovesRouteKey, Name = nameof(GetAllMovesForCharacterWhereName))]
        public IHttpActionResult GetAllMovesForCharacterWhereName(string name, [FromUri] string fields = "")
        {
            var moves = _characterService.GetAllMovesWhereCharacterNameIs(name, fields);
            return Result(moves);
        }

        [Route(CharactersRouteKey + IdRouteKey + MovesRouteKey, Name = nameof(GetAllMovesForCharacterWhereOwnerId))]
        public IHttpActionResult GetAllMovesForCharacterWhereOwnerId(int id, [FromUri] string fields = "")
        {
            var moves = _characterService.GetAllMovesWhereCharacterOwnerIdIs(id, fields);
            return Result(moves);
        }

        [Route(CharactersRouteKey + NameRouteKey + MovementsRouteKey, Name = nameof(GetAllMovementsForCharacterWhereName))]
        public IHttpActionResult GetAllMovementsForCharacterWhereName(string name, [FromUri] string fields = "")
        {
            var movements = _characterService.GetAllMovementsWhereCharacterNameIs(name, fields);
            return Result(movements);
        }

        [Route(CharactersRouteKey + NameRouteKey + MovementsRouteKey + SearchRouteKey, Name = nameof(GetAllMovementsForCharacterByNameWhereFilter))]
        public IHttpActionResult GetAllMovementsForCharacterByNameWhereFilter([FromUri] MovementFilterResourceQuery query)
        {
            var movements = _characterService.GetAllMovementsWhereCharacterNameIsFilteredBy(query);
            return Result(movements);
        }

        [Route(CharactersRouteKey + IdRouteKey + MovementsRouteKey + SearchRouteKey, Name = nameof(GetAllMovementsForCharacterByOwnerIdWhereFilter))]
        public IHttpActionResult GetAllMovementsForCharacterByOwnerIdWhereFilter([FromUri] MovementFilterResourceQuery query)
        {
            var movements = _characterService.GetAllMovementsWhereCharacterOwnerIdIsFilteredBy(query);
            return Result(movements);
        }

        [Route(CharactersRouteKey + IdRouteKey + MovementsRouteKey, Name = nameof(GetAllMovementsForCharacterWhereOwnerId))]
        public IHttpActionResult GetAllMovementsForCharacterWhereOwnerId(int id, [FromUri] string fields = "")
        {
            var movements = _characterService.GetAllMovementsWhereCharacterOwnerIdIs(id, fields);
            return Result(movements);
        }

        [SingleCharacterResourceHalSupport]
        [Route(CharactersRouteKey + NameRouteKey + DetailsRouteKey, Name = nameof(GetDetailsForCharacterByName))]
        public IHttpActionResult GetDetailsForCharacterByName(string name, [FromUri] string fields = "")
        {
            var dto = _characterService.GetCharacterDetailsWhereCharacterOwnerIs(name, fields);
            return Result(dto);
        }

        [SingleCharacterResourceHalSupport]
        [Route(CharactersRouteKey + IdRouteKey + DetailsRouteKey, Name = nameof(GetDetailsForCharacterByOwnerId))]
        public IHttpActionResult GetDetailsForCharacterByOwnerId(int id, [FromUri] string fields = "")
        {
            var dto = _characterService.GetCharacterDetailsWhereCharacterOwnerIdIs(id, fields);
            return Result(dto);
        }

        [Route(CharactersRouteKey + NameRouteKey + CharacterAttributesRouteKey, Name = nameof(GetAttributesForCharacterByName))]
        public IHttpActionResult GetAttributesForCharacterByName(string name, [FromUri] string fields = "")
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterNameIs(name, fields);
            return Result(attributeRows);
        }

        [Route(CharactersRouteKey + IdRouteKey + CharacterAttributesRouteKey, Name = nameof(GetAttributesForCharacterByOwnerId))]
        public IHttpActionResult GetAttributesForCharacterByOwnerId(int id, [FromUri] string fields = "")
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterOwnerIdIs(id, fields);
            return Result(attributeRows);
        }

        [Route(CharactersRouteKey + NameRouteKey + DetailedMovesRouteKey, Name = nameof(GetDetailedMovesForCharacterByName))]
        public IHttpActionResult GetDetailedMovesForCharacterByName(string name, [FromUri] string fields = "")
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterNameIs(name, fields);
            return Result(detailedMoves);
        }

        [Route(CharactersRouteKey + IdRouteKey + DetailedMovesRouteKey, Name = nameof(GetDetailedMovesForCharacterByOwnerId))]
        public IHttpActionResult GetDetailedMovesForCharacterByOwnerId(int id, [FromUri] string fields = "")
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterOwnerIdIs(id, fields);
            return Result(detailedMoves);
        }
    }
}