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

        [Route(CharacterAttributesRouteKey)]
        public override IHttpActionResult GetAll([FromUri] string fields = "")
        {
            var content = _characterAttributeRowService.GetAll(fields);
            return Result(content);
        }

        [Route(CharacterAttributesRouteKey + "/{id}")]
        public IHttpActionResult GetById(string id, [FromUri] string fields = "")
        {
            var content = _characterAttributeRowService.GetSingleById(id, fields);
            return Result(content);
        }

        [Route(CharacterAttributesRouteKey + "/name/{name}")]
        public override IHttpActionResult GetAllWhereName(string name, string fields = "")
        {
            var content = _characterAttributeRowService.GetAllWhereName(name, fields);
            return Result(content);
        }
    }
}