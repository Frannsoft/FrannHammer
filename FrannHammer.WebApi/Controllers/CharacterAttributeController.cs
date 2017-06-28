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
        public IHttpActionResult GetAllCharacterAttributes([FromUri] string fields = "")
        {
            var content = _characterAttributeRowService.GetAll(fields);
            return Result(content);
        }

        [Route(CharacterAttributesRouteKey + "/{id}", Name = nameof(GetCharacterAttributeById))]
        public IHttpActionResult GetCharacterAttributeById(string id, [FromUri] string fields = "")
        {
            var content = _characterAttributeRowService.GetSingleByInstanceId(id, fields);
            return Result(content);
        }

        [Route(CharacterAttributesRouteKey + "/name/{name}", Name = nameof(GetSingleCharacterAttributeByName))]
        public IHttpActionResult GetSingleCharacterAttributeByName(string name, string fields = "")
        {
            var content = _characterAttributeRowService.GetAllWhereName(name, fields);
            return Result(content);
        }
    }
}