using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Utility;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    [RoutePrefix(DefaultRoutePrefix)]
    public class CharacterAttributeController : BaseApiController
    {
        private const string CharacterAttributesRouteKey = "characterattributes";
        private readonly ICharacterAttributeRowService _characterAttributeRowService;

        public CharacterAttributeController(ICharacterAttributeRowService characterAttributeRowService)
        {
            Guard.VerifyObjectNotNull(characterAttributeRowService, nameof(characterAttributeRowService));
            _characterAttributeRowService = characterAttributeRowService;
        }

        [Route(CharacterAttributesRouteKey, Name = nameof(GetAllCharacterAttributes))]
        public IHttpActionResult GetAllCharacterAttributes()
        {
            var content = _characterAttributeRowService.GetAll();
            return Result(content);
        }

        [Route(CharacterAttributesRouteKey + "/{id}", Name = nameof(GetCharacterAttributeById))]
        public IHttpActionResult GetCharacterAttributeById(string id)
        {
            var content = _characterAttributeRowService.GetSingleByInstanceId(id);
            return Result(content);
        }

        [Route(CharacterAttributesRouteKey + "/name/{name}", Name = nameof(GetAllCharacterAttributesWithName))]
        public IHttpActionResult GetAllCharacterAttributesWithName(string name)
        {
            var content = _characterAttributeRowService.GetAllWhereName(name);
            return Result(content);
        }

        [Route(CharacterAttributesRouteKey + "/types", Name = nameof(GetAllCharacterAttributeTypes))]
        public IHttpActionResult GetAllCharacterAttributeTypes()
        {
            var content = _characterAttributeRowService.GetAllTypes();
            return Result(content);
        }
    }
}