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
        public override IHttpActionResult GetById(string id, [FromUri]string fields = "")
        {
            var move = _moveService.GetSingleById(id, fields);
            return Result(move);
        }

        [Route(MovesRouteKey)]
        public override IHttpActionResult GetAll([FromUri]string fields = "")
        {
            var moves = _moveService.GetAll(fields);
            return Result(moves);
        }

        [Route(MovesRouteKey + "/name/{name}")]
        public override IHttpActionResult GetAllWhereName(string name, string fields = "")
        {
            var content = _moveService.GetAllWhereName(name, fields);
            return Result(content);
        }

        [Route(MovesRouteKey + "/name/{name}/{property}")]
        public IHttpActionResult GetAllPropertyDataForMoveByName(string name, string property, string fields = "")
        {
            var content = _moveService.GetAllPropertyDataWhereName(name, property, fields);
            return Result(content);
        }
    }
}