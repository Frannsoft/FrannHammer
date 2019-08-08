using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using FrannHammer.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static FrannHammer.NetCore.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.NetCore.WebApi.Controllers
{
    [Route(DefaultRoutePrefix)]
    public class MovementController : BaseApiController
    {
        private const string MovementsRouteKey = "movements";
        private readonly IMovementService _movementService;
        private readonly IEnrichmentProvider _enrichmentProvider;

        public MovementController(IMovementService movementService, IEnrichmentProvider enrichmentProvider)
        {
            Guard.VerifyObjectNotNull(movementService, nameof(movementService));
            _movementService = movementService;
            _enrichmentProvider = enrichmentProvider;
        }

        /// <summary>
        /// Get all movement data.
        /// </summary>
        [HttpGet(MovementsRouteKey, Name = nameof(GetAllMovements))]
        public IActionResult GetAllMovements()
        {
            var content = _movementService.GetAll();
            var resources = _enrichmentProvider.EnrichManyMovements(content);
            return Result(resources);
        }

        /// <summary>
        /// Get a specific <see cref="IMovement"/>.
        /// </summary>
        /// <param name="id"></param>
        [HttpGet(MovementsRouteKey + "/{id}", Name = nameof(GetMovementById))]
        public IActionResult GetMovementById(string id)
        {
            var content = _movementService.GetSingleByInstanceId(id);
            var resource = _enrichmentProvider.EnrichMovement(content);
            return Result(resource);
        }

        [HttpGet(MovementsRouteKey + "/name/{name}", Name = nameof(GetSingleMovementByName))]
        public IActionResult GetSingleMovementByName(string name)
        {
            var content = _movementService.GetAllWhereName(name);
            var resource = _enrichmentProvider.EnrichManyMovements(content);
            return Result(resource);
        }
    }
}