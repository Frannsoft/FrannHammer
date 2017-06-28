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

        [Route(MovesRouteKey + "/{id}", Name = nameof(GetMoveById))]
        public IHttpActionResult GetMoveById(string id, [FromUri]string fields = "")
        {
            var move = _moveService.GetSingleByInstanceId(id, fields);
            return Result(move);
        }

        [Route(MovesRouteKey, Name = nameof(GetAllMoves))]
        public IHttpActionResult GetAllMoves([FromUri]string fields = "")
        {
            var moves = _moveService.GetAll(fields);
            return Result(moves);
        }

        [Route(MovesRouteKey + "/name/{name}", Name = nameof(GetSingleMoveByName))]
        public IHttpActionResult GetSingleMoveByName(string name, string fields = "")
        {
            var content = _moveService.GetAllWhereName(name, fields);
            return Result(content);
        }

        [Route(MovesRouteKey + "/name/{name}/{property}", Name = nameof(GetAllPropertyDataForMoveByName))]
        public IHttpActionResult GetAllPropertyDataForMoveByName(string name, string property, string fields = "")
        {
            var content = _moveService.GetAllPropertyDataWhereName(name, property, fields);
            return Result(content);
        }

        [Route(MovesRouteKey + "/{id}/{property}", Name = nameof(GetAllPropertyDataForMoveById))]
        public IHttpActionResult GetAllPropertyDataForMoveById(string id, string property, string fields = "")
        {
            var content = _moveService.GetPropertyDataWhereId(id, property, fields);
            return Result(content);
        }
    }
}