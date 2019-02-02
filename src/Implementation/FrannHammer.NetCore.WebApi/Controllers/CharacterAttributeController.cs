using FrannHammer.Api.Services.Contracts;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using FrannHammer.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static FrannHammer.NetCore.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.NetCore.WebApi.Controllers
{
    [Route(DefaultRoutePrefix)]
    public class CharacterAttributeController : BaseApiController
    {
        private const string CharacterAttributesRouteKey = "characterattributes";
        private readonly ICharacterAttributeRowService _characterAttributeRowService;
        private readonly IEnrichmentProvider _enrichmentProvider;

        public CharacterAttributeController(ICharacterAttributeRowService characterAttributeRowService, IEnrichmentProvider enrichmentProvider)
        {
            Guard.VerifyObjectNotNull(characterAttributeRowService, nameof(characterAttributeRowService));
            _characterAttributeRowService = characterAttributeRowService;
            _enrichmentProvider = enrichmentProvider;
        }

        [HttpGet(CharacterAttributesRouteKey, Name = nameof(GetAllCharacterAttributes))]
        public IActionResult GetAllCharacterAttributes()
        {
            var content = _characterAttributeRowService.GetAll();
            var resource = _enrichmentProvider.EnrichManyCharacterAttributeRowResources(content);
            return Result(resource);
        }

        [HttpGet(CharacterAttributesRouteKey + "/{id}", Name = nameof(GetCharacterAttributeById))]
        public IActionResult GetCharacterAttributeById(string id)
        {
            var content = _characterAttributeRowService.GetSingleByInstanceId(id);
            var resource = _enrichmentProvider.EnrichCharacterAttributeRow(content);
            return Result(resource);
        }

        [HttpGet(CharacterAttributesRouteKey + "/name/{name}", Name = nameof(GetAllCharacterAttributesWithName))]
        public IActionResult GetAllCharacterAttributesWithName(string name)
        {
            var content = _characterAttributeRowService.GetAllWhereName(name);
            var resource = _enrichmentProvider.EnrichManyCharacterAttributeRowResources(content);
            return Result(resource);
        }

        [HttpGet(CharacterAttributesRouteKey + "/types", Name = nameof(GetAllCharacterAttributeTypes))]
        public IActionResult GetAllCharacterAttributeTypes()
        {
            var content = _characterAttributeRowService.GetAllTypes();
            var resources = _enrichmentProvider.EnrichManyCharacterAttributeNames(content);
            return Result(resources);
        }
    }
}