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
        private readonly ICharacterAttributeService _characterAttributeService;

        public CharacterAttributeController(ICharacterAttributeService characterAttributeService)
        {
            Guard.VerifyObjectNotNull(characterAttributeService, nameof(characterAttributeService));
            _characterAttributeService = characterAttributeService;
        }

        [Route(CharacterAttributesRouteKey)]
        public IHttpActionResult GetCharacterAttributes([FromUri] string fields = "")
        {
            var content = _characterAttributeService.GetAll(fields);
            return Result(content);
        }

        [Route(CharacterAttributesRouteKey + "/{id}")]
        public IHttpActionResult GetCharacterAttribute(int id, [FromUri] string fields = "")
        {
            var content = _characterAttributeService.Get(id, fields);
            return Result(content);
        }
    }
}