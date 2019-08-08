using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Utility;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    [RoutePrefix(DefaultRoutePrefix)]
    public class UniqueDataController : BaseApiController
    {
        private const string IdRouteKey = "/{id}";
        private const string UniqueDataRouteKey = "uniqueproperties";

        private readonly IUniqueDataService _uniqueDataService;

        public UniqueDataController(IUniqueDataService uniqueDataService)
        {
            Guard.VerifyObjectNotNull(uniqueDataService, nameof(uniqueDataService));
            _uniqueDataService = uniqueDataService;
        }

        [Route(UniqueDataRouteKey + IdRouteKey, Name = nameof(GetAllUniquePropertiesById))]
        public IHttpActionResult GetAllUniquePropertiesById(string id)
        {
            var uniqueProperties = _uniqueDataService.GetSingleByInstanceId(id);
            return Result(uniqueProperties);
        }

        [Route(UniqueDataRouteKey, Name = nameof(GetAllUniqueProperties))]
        public IHttpActionResult GetAllUniqueProperties()
        {
            var uniqueProperties = _uniqueDataService.GetAll();
            return Result(uniqueProperties);
        }
    }
}