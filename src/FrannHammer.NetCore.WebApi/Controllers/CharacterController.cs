using FrannHammer.Api.Services.Contracts;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using FrannHammer.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System.Threading.Tasks;
using static FrannHammer.NetCore.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.NetCore.WebApi.Controllers
{
    [Route(DefaultRoutePrefix)]
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
        private readonly IEnrichmentProvider _enrichmentProvider;

        private string _game;

        public CharacterController(ICharacterService characterService, IEnrichmentProvider enrichmentProvider)
        {
            Guard.VerifyObjectNotNull(characterService, nameof(characterService));
            _characterService = characterService;
            _enrichmentProvider = enrichmentProvider;
        }

        private string ReturnGameFromQueryString()
        {
            HttpContext.Request.Query.TryGetValue("game", out StringValues game);
            return game.ToString();
        }

        [HttpGet(CharactersRouteKey + IdRouteKey)]
        public IActionResult GetCharacterByOwnerId(int id)
        {
            var character = _characterService.GetSingleByOwnerId(id);
            var resource = _enrichmentProvider.EnrichCharacter(character);
            return Result(resource);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey)]
        public async Task<IActionResult> GetSingleCharacterByName(string name)
        {
            var character = _characterService.GetSingleByName(name);

            var resource = _enrichmentProvider.EnrichCharacter(character);
            return Result(resource);
        }

        [HttpGet(CharactersRouteKey)]
        public IActionResult GetAllCharacters()
        {
            string game = ReturnGameFromQueryString();

            var characters = _characterService.GetAll().ToList();

            var resources = _enrichmentProvider.EnrichManyCharacters(characters);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + ThrowsRouteKey)]
        public IActionResult GetAllThrowsForCharacterWhereName(string name)
        {
            var throws = _characterService.GetAllThrowsWhereCharacterNameIs(name);
            var resources = _enrichmentProvider.EnrichManyMoves(throws);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + ThrowsRouteKey)]
        public IActionResult GetAllThrowsForCharacterWhereOwnerId(int id)
        {
            var throws = _characterService.GetAllThrowsWhereCharacterOwnerIdIs(id);
            var resources = _enrichmentProvider.EnrichManyMoves(throws);
            return Result(resources);
        }

        //[HttpGet(CharactersRouteKey + NameRouteKey + MovesRouteKey + SearchRouteKey, Name = nameof(GetAllMovesForCharacterByNameWhereFilter))]
        //public IActionResult GetAllMovesForCharacterByNameWhereFilter([FromQuery] MoveFilterResourceQuery query)
        //{
        //    var moves = _characterService.GetAllMovesForCharacterByOwnerIdFilteredBy(query);
        //    return Result(moves);
        //}

        //[HttpGet(CharactersRouteKey + IdRouteKey + MovesRouteKey + SearchRouteKey, Name = nameof(GetAllMovesForCharacterByIdWhereFilter))]
        //public IActionResult GetAllMovesForCharacterByIdWhereFilter([FromQuery] MoveFilterResourceQuery query)
        //{
        //    var moves = _characterService.GetAllMovesForCharacterByNameFilteredBy(query);
        //    return Result(moves);
        //}

        [HttpGet(CharactersRouteKey + NameRouteKey + MovesRouteKey, Name = nameof(GetAllMovesForCharacterWhereName))]
        public IActionResult GetAllMovesForCharacterWhereName(string name)
        {
            var moves = _characterService.GetAllMovesWhereCharacterNameIs(name);
            var resources = _enrichmentProvider.EnrichManyMoves(moves);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + MovesRouteKey)]
        public IActionResult GetAllMovesForCharacterWhereOwnerId(int id)
        {
            var moves = _characterService.GetAllMovesWhereCharacterOwnerIdIs(id);
            var resources = _enrichmentProvider.EnrichManyMoves(moves);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + MovementsRouteKey)]
        public IActionResult GetAllMovementsForCharacterWhereName(string name)
        {
            var movements = _characterService.GetAllMovementsWhereCharacterNameIs(name);
            var resources = _enrichmentProvider.EnrichManyMovements(movements);
            return Result(resources);
        }

        //[HttpGet(CharactersRouteKey + NameRouteKey + MovementsRouteKey + SearchRouteKey, Name = nameof(GetAllMovementsForCharacterByNameWhereFilter))]
        //public IActionResult GetAllMovementsForCharacterByNameWhereFilter([FromUri] MovementFilterResourceQuery query)
        //{
        //    var movements = _characterService.GetAllMovementsWhereCharacterNameIsFilteredBy(query);
        //    return Result(movements);
        //}

        //[HttpGet(CharactersRouteKey + IdRouteKey + MovementsRouteKey + SearchRouteKey, Name = nameof(GetAllMovementsForCharacterByOwnerIdWhereFilter))]
        //public IActionResult GetAllMovementsForCharacterByOwnerIdWhereFilter([FromUri] MovementFilterResourceQuery query)
        //{
        //    var movements = _characterService.GetAllMovementsWhereCharacterOwnerIdIsFilteredBy(query);
        //    return Result(movements);
        //}

        [HttpGet(CharactersRouteKey + IdRouteKey + MovementsRouteKey)]
        public IActionResult GetAllMovementsForCharacterWhereOwnerId(int id)
        {
            var movements = _characterService.GetAllMovementsWhereCharacterOwnerIdIs(id);
            var resources = _enrichmentProvider.EnrichManyMovements(movements);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + DetailsRouteKey)]
        public IActionResult GetDetailsForCharacterByName(string name)
        {
            var dto = _characterService.GetCharacterDetailsWhereCharacterOwnerIs(name);
            return Result(dto);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + DetailsRouteKey)]
        public IActionResult GetDetailsForCharacterByOwnerId(int id)
        {
            var dto = _characterService.GetCharacterDetailsWhereCharacterOwnerIdIs(id);
            return Result(dto);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + CharacterAttributesRouteKey)]
        public IActionResult GetAttributesForCharacterByName(string name)
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterNameIs(name);
            var resources = _enrichmentProvider.EnrichManyCharacterAttributeRowResources(attributeRows);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + CharacterAttributesRouteKey)]
        public IActionResult GetAttributesForCharacterByOwnerId(int id)
        {
            var attributeRows = _characterService.GetAllAttributesWhereCharacterOwnerIdIs(id);
            var resources = _enrichmentProvider.EnrichManyCharacterAttributeRowResources(attributeRows);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + DetailedMovesRouteKey)]
        public IActionResult GetDetailedMovesForCharacterByName(string name)
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterNameIs(name);
            return Result(detailedMoves);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + DetailedMovesRouteKey)]
        public IActionResult GetDetailedMovesForCharacterByOwnerId(int id)
        {
            var detailedMoves = _characterService.GetDetailedMovesWhereCharacterOwnerIdIs(id);
            return Result(detailedMoves);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + UniquePropertiesRouteKey)]
        public IActionResult GetUniquePropertiesForCharacterByOwnerId(int id)
        {
            var uniqueProperties = _characterService.GetUniquePropertiesWhereCharacterOwnerIdIs(id);
            var resources = _enrichmentProvider.EnrichManyUniqueDatas(uniqueProperties);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + UniquePropertiesRouteKey)]
        public IActionResult GetUniquePropertiesForCharacterByName(string name)
        {
            var uniqueProperties = _characterService.GetUniquePropertiesWhereCharacterNameIs(name);
            var resources = _enrichmentProvider.EnrichManyUniqueDatas(uniqueProperties);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + CharacterAttributesRouteKey + AttributeNameKey)]
        public IActionResult GetCharacterAttributeWithNameForCharacterByOwnerId(int id, string attributename)
        {
            var attributeRow = _characterService.GetAttributesOfNameWhereCharacterOwnerIdIs(attributename, id);
            var resources = _enrichmentProvider.EnrichManyCharacterAttributeRowResources(attributeRow);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + CharacterAttributesRouteKey + AttributeNameKey)]
        public IActionResult GetCharacterAttributeWithNameForCharacterByName(string name, string attributename)
        {
            var attributeRow = _characterService.GetAttributesOfNameWhereCharacterNameIs(name, attributename);
            var resources = _enrichmentProvider.EnrichManyCharacterAttributeRowResources(attributeRow);
            return Result(resources);
        }
    }
}
