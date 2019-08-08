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
        private const string UniquePropertiesRouteKey = "/uniqueproperties";
        private const string DetailedMovesRouteKey = "/detailedmoves";
        private const string SearchRouteKey = "/search";
        private const string AttributeNameKey = "/name/{attributename}";
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            Guard.VerifyObjectNotNull(characterService, nameof(characterService));
            _characterService = characterService;
        }

        [Route(CharactersRouteKey + IdRouteKey, Name = nameof(GetCharacterByOwnerId))]
        public IHttpActionResult GetCharacterByOwnerId(int id)
        {
            var character = _characterService.GetSingleByOwnerId(id);
            return Result(character);
        }

        [Route(CharactersRouteKey + NameRouteKey, Name = nameof(GetSingleCharacterByName))]
        public IHttpActionResult GetSingleCharacterByName(string name)
        {
            var character = _characterService.GetSingleByName(name);
            return Result(character);
        }

        [Route(CharactersRouteKey, Name = nameof(GetAllCharacters))]
        public IHttpActionResult GetAllCharacters()
        {
            var characters = _characterService.GetAll();
            return Result(characters);
        }

        [Route(CharactersRouteKey + NameRouteKey + ThrowsRouteKey, Name = nameof(GetAllThrowsForCharacterWhereName))]
        public IHttpActionResult GetAllThrowsForCharacterWhereName(string name)
        {
            var throws = _characterService.GetAllThrowsWhereCharacterNameIs(name);
            return Result(throws);
        }

        [Route(CharactersRouteKey + IdRouteKey + ThrowsRouteKey, Name = nameof(GetAllThrowsForCharacterWhereOwnerId))]
        public IHttpActionResult GetAllThrowsForCharacterWhereOwnerId(int id)
        {
            var throws = _characterService.GetAllThrowsWhereCharacterOwnerIdIs(id);
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
        public IHttpActionResult GetAllMovesForCharacterWhereName(string name)
        {
            var moves = _characterService.GetAllMovesWhereCharacterNameIs(name);
            return Result(moves);
        }

        [Route(CharactersRouteKey + IdRouteKey + MovesRouteKey, Name = nameof(GetAllMovesForCharacterWhereOwnerId))]
        public IHttpActionResult GetAllMovesForCharacterWhereOwnerId(int id)
        {
            var moves = _characterService.GetAllMovesWhereCharacterOwnerIdIs(id);
            return Result(moves);
        }

        [Route(CharactersRouteKey + NameRouteKey + MovementsRouteKey, Name = nameof(GetAllMovementsForCharacterWhereName))]
        public IHttpActionResult GetAllMovementsForCharacterWhereName(string name)
        {
            var movements = _characterService.GetAllMovementsWhereCharacterNameIs(name);
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
        public IHttpActionResult GetAllMovementsForCharacterWhereOwnerId(int id)
        {
            var movements = _characterService.GetAllMovementsWhereCharacterOwnerIdIs(id);
            return Result(movements);
        }

        [Route(CharactersRouteKey + NameRouteKey + DetailsRouteKey, Name = nameof(GetDetailsForCharacterByName))]
        public IHttpActionResult GetDetailsForCharacterByName(string name)
        {
            var dto = _characterService.GetCharacterDetailsWhereCharacterOwnerIs(name);
            return Result(dto);
        }

        [Route(CharactersRouteKey + IdRouteKey + DetailsRouteKey, Name = nameof(GetDetailsForCharacterByOwnerId))]
        public IHttpActionResult GetDetailsForCharacterByOwnerId(int id)
        {
            var dto = _characterService.GetCharacterDetailsWhereCharacterOwnerIdIs(id);
            return Result(dto);
        }

        [Route(CharactersRouteKey + NameRouteKey + CharacterAttributesRouteKey, Name = nameof(GetAttributesForCharacterByName))]
        public IHttpActionResult GetAttributesForCharacterByName(string name)
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterNameIs(name);
            return Result(attributeRows);
        }

        [Route(CharactersRouteKey + IdRouteKey + CharacterAttributesRouteKey, Name = nameof(GetAttributesForCharacterByOwnerId))]
        public IHttpActionResult GetAttributesForCharacterByOwnerId(int id)
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterOwnerIdIs(id);
            return Result(attributeRows);
        }

        [Route(CharactersRouteKey + NameRouteKey + DetailedMovesRouteKey, Name = nameof(GetDetailedMovesForCharacterByName))]
        public IHttpActionResult GetDetailedMovesForCharacterByName(string name)
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterNameIs(name);
            return Result(detailedMoves);
        }

        [Route(CharactersRouteKey + IdRouteKey + DetailedMovesRouteKey, Name = nameof(GetDetailedMovesForCharacterByOwnerId))]
        public IHttpActionResult GetDetailedMovesForCharacterByOwnerId(int id)
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterOwnerIdIs(id);
            return Result(detailedMoves);
        }

        [Route(CharactersRouteKey + IdRouteKey + UniquePropertiesRouteKey, Name = nameof(GetUniquePropertiesForCharacterByOwnerId))]
        public IHttpActionResult GetUniquePropertiesForCharacterByOwnerId(int id)
        {
            var uniqueProperties = _characterService.GetUniquePropertiesWhereCharacterOwnerIdIs(id);
            return Result(uniqueProperties);
        }

        [Route(CharactersRouteKey + NameRouteKey + UniquePropertiesRouteKey, Name = nameof(GetUniquePropertiesForCharacterByName))]
        public IHttpActionResult GetUniquePropertiesForCharacterByName(string name)
        {
            var uniqueProperties = _characterService.GetUniquePropertiesWhereCharacterNameIs(name);
            return Result(uniqueProperties);
        }

        [Route(CharactersRouteKey + IdRouteKey + CharacterAttributesRouteKey + AttributeNameKey, Name = nameof(GetCharacterAttributeWithNameForCharacterByOwnerId))]
        public IHttpActionResult GetCharacterAttributeWithNameForCharacterByOwnerId(int id, string attributename)
        {
            var attributeRow = _characterService.GetAttributesOfNameWhereCharacterOwnerIdIs(attributename, id);
            return Result(attributeRow);
        }

        [Route(CharactersRouteKey + NameRouteKey + CharacterAttributesRouteKey + AttributeNameKey, Name = nameof(GetCharacterAttributeWithNameForCharacterByName))]
        public IHttpActionResult GetCharacterAttributeWithNameForCharacterByName(string name, string attributename)
        {
            var attributeRow = _characterService.GetAttributesOfNameWhereCharacterNameIs(name, attributename);
            return Result(attributeRow);
        }
    }
}