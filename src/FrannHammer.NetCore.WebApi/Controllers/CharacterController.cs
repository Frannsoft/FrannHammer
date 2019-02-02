using FrannHammer.Api.Services.Contracts;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using FrannHammer.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System.Linq;
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
        private const string CharacterAttributesRouteKey = "/characterattributes";
        private const string UniquePropertiesRouteKey = "/uniqueproperties";
        private const string AttributeNameKey = "/name/{attributename}";
        private readonly ICharacterService _characterService;
        private readonly IEnrichmentProvider _enrichmentProvider;

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
        public IActionResult GetSingleCharacterByName(string name)
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
        public IActionResult GetAllThrowsForCharacterWhereName(string name, bool expand = false)
        {
            var throws = _characterService.GetAllThrowsWhereCharacterNameIs(name);
            var resources = _enrichmentProvider.EnrichManyMoves(throws);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + ThrowsRouteKey)]
        public IActionResult GetAllThrowsForCharacterWhereOwnerId(int id, bool expand = false)
        {
            var throws = _characterService.GetAllThrowsWhereCharacterOwnerIdIs(id);
            var resources = _enrichmentProvider.EnrichManyMoves(throws);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + MovesRouteKey, Name = nameof(GetAllMovesForCharacterWhereName))]
        public IActionResult GetAllMovesForCharacterWhereName(string name, bool expand = false)
        {
            var moves = _characterService.GetAllMovesWhereCharacterNameIs(name);
            var resources = _enrichmentProvider.EnrichManyMoves(moves, expand);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + MovesRouteKey)]
        public IActionResult GetAllMovesForCharacterWhereOwnerId(int id, bool expand = false)
        {
            var moves = _characterService.GetAllMovesWhereCharacterOwnerIdIs(id);
            var resources = _enrichmentProvider.EnrichManyMoves(moves, expand);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + NameRouteKey + MovementsRouteKey)]
        public IActionResult GetAllMovementsForCharacterWhereName(string name)
        {
            var movements = _characterService.GetAllMovementsWhereCharacterNameIs(name);
            var resources = _enrichmentProvider.EnrichManyMovements(movements);
            return Result(resources);
        }

        [HttpGet(CharactersRouteKey + IdRouteKey + MovementsRouteKey)]
        public IActionResult GetAllMovementsForCharacterWhereOwnerId(int id)
        {
            var movements = _characterService.GetAllMovementsWhereCharacterOwnerIdIs(id);
            var resources = _enrichmentProvider.EnrichManyMovements(movements);
            return Result(resources);
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
