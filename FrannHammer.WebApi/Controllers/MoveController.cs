using System.Web.Http;
using FrannHammer.Api.Services.Contracts;
using FrannHammer.Utility;
using static FrannHammer.WebApi.Controllers.ApiControllerBaseRoutingConstants;

namespace FrannHammer.WebApi.Controllers
{
    [RoutePrefix(DefaultRoutePrefix)]
    public class MoveController : BaseApiController
    {
        private const string MovesRouteKey = "moves";
        private readonly IMoveService _moveService;

        public MoveController(IMoveService moveService)
        {
            Guard.VerifyObjectNotNull(moveService, nameof(moveService));
            _moveService = moveService;
        }

        [Route(MovesRouteKey + "/{id}")]
        public IHttpActionResult GetMove(string id, string fields = "")
        {
            var move = _moveService.Get(id, fields);
            return Result(move);
        }

        [Route(MovesRouteKey)]
        public IHttpActionResult GetAllMoves(string fields = "")
        {
            var moves = _moveService.GetAll(fields);
            return Result(moves);
        }
    }
}