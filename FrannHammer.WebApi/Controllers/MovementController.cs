﻿using System.Web.Http;
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
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        [Route(MovementsRouteKey, Name = "GetAllMovements")]
        public IHttpActionResult GetAllMovements([FromUri] string fields = "")
        {
            var content = _movementService.GetAll(fields);
            return Result(content);
        }

        /// <summary>
        /// Get a specific <see cref="IMovement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [Route(MovementsRouteKey + "/{id}")]
        public IHttpActionResult GetMovement(string id, [FromUri] string fields = "")
        {
            var content = _movementService.Get(id, fields);
            return Result(content);
        }
    }
}