using FrannHammer.Api.Services.Contracts;
using FrannHammer.NetCore.WebApi.HypermediaServices;
using FrannHammer.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static FrannHammer.NetCore.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.NetCore.WebApi.Controllers
{
    [Route(DefaultRoutePrefix)]
    public class MoveController : BaseApiController
    {
        private const string MovesRouteKey = "moves";
        private readonly IMoveService _moveService;
        private readonly IEnrichmentProvider _enrichmentProvider;

        public MoveController(IMoveService moveService, IEnrichmentProvider enrichmentProvider)
        {
            Guard.VerifyObjectNotNull(moveService, nameof(moveService));
            _moveService = moveService;
            _enrichmentProvider = enrichmentProvider;
        }

        [HttpGet(MovesRouteKey + "/{id}", Name = nameof(GetMoveById))]
        public IActionResult GetMoveById(string id, bool expand = false)
        {
            var move = _moveService.GetSingleByInstanceId(id);
            var resource = _enrichmentProvider.EnrichMove(move, expand);
            return Result(resource);
        }

        [HttpGet(MovesRouteKey, Name = nameof(GetAllMoves))]
        public IActionResult GetAllMoves(bool expand = false)
        {
            var moves = _moveService.GetAll();
            var resources = _enrichmentProvider.EnrichManyMoves(moves, expand);
            return Result(resources);
        }

        [HttpGet(MovesRouteKey + "/name/{name}", Name = nameof(GetSingleMoveByName))]
        public IActionResult GetSingleMoveByName(string name, bool expand = false)
        {
            var content = _moveService.GetAllWhereName(name);
            var resources = _enrichmentProvider.EnrichManyMoves(content, expand);
            return Result(resources);
        }

        [HttpGet(MovesRouteKey + "/name/{name}/{property}", Name = nameof(GetAllPropertyDataForMoveByName))]
        public IActionResult GetAllPropertyDataForMoveByName(string name, string property)
        {
            var content = _moveService.GetAllPropertyDataWhereName(name, property);
            return Result(content);
        }

        [HttpGet(MovesRouteKey + "/{id}/{property}", Name = nameof(GetAllPropertyDataForMoveById))]
        public IActionResult GetAllPropertyDataForMoveById(string id, string property)
        {
            var content = _moveService.GetPropertyDataWhereId(id, property);
            return Result(content);
        }
    }
}