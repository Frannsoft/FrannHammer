using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Domain.Contracts;
using FrannHammer.Utility;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    [RoutePrefix(DefaultRoutePrefix)]
    public class MovementController : BaseApiController
    {
        private const string MovementsRouteKey = "movements";
        private readonly IMovementService _movementService;

        public MovementController(IMovementService movementService)
        {
            Guard.VerifyObjectNotNull(movementService, nameof(movementService));
            _movementService = movementService;
        }

        /// <summary>
        /// Get all movement data.
        /// </summary>
        [Route(MovementsRouteKey, Name = nameof(GetAllMovements))]
        public IHttpActionResult GetAllMovements()
        {
            var content = _movementService.GetAll();
            return Result(content);
        }

        /// <summary>
        /// Get a specific <see cref="IMovement"/>.
        /// </summary>
        /// <param name="id"></param>
        [Route(MovementsRouteKey + "/{id}", Name = nameof(GetMovementById))]
        public IHttpActionResult GetMovementById(string id)
        {
            var content = _movementService.GetSingleByInstanceId(id);
            return Result(content);
        }

        [Route(MovementsRouteKey + "/name/{name}", Name = nameof(GetSingleMovementByName))]
        public IHttpActionResult GetSingleMovementByName(string name)
        {
            var content = _movementService.GetAllWhereName(name);
            return Result(content);
        }
    }
}