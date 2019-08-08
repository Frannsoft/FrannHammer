using FrannHammer.Api.Services.Contracts;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using FrannHammer.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static FrannHammer.NetCore.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.NetCore.WebApi.Controllers
{
    [Route(DefaultRoutePrefix)]
    public class UniqueDataController : BaseApiController
    {
        private const string IdRouteKey = "/{id}";
        private const string UniqueDataRouteKey = "uniqueproperties";

        private readonly IUniqueDataService _uniqueDataService;
        private readonly IEnrichmentProvider _enrichmentProvider;

        public UniqueDataController(IUniqueDataService uniqueDataService, IEnrichmentProvider enrichmentProvider)
        {
            Guard.VerifyObjectNotNull(uniqueDataService, nameof(uniqueDataService));
            _uniqueDataService = uniqueDataService;
            _enrichmentProvider = enrichmentProvider;
        }

        [HttpGet(UniqueDataRouteKey + IdRouteKey, Name = nameof(GetAllUniquePropertiesById))]
        public IActionResult GetAllUniquePropertiesById(string id)
        {
            var uniqueProperties = _uniqueDataService.GetSingleByInstanceId(id);
            var resource = _enrichmentProvider.EnrichUniqueData(uniqueProperties);
            return Result(resource);
        }

        [HttpGet(UniqueDataRouteKey, Name = nameof(GetAllUniqueProperties))]
        public IActionResult GetAllUniqueProperties()
        {
            var uniqueProperties = _uniqueDataService.GetAll();
            var resources = _enrichmentProvider.EnrichManyUniqueDatas(uniqueProperties);
            return Result(resources);
        }
    }
}