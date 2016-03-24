using Kurogane.Data.RestApi.DTOs;
using Kurogane.Data.RestApi.Models;
using System.Linq;
using System.Net;
using System.Web.Http;
using Kurogane.Data.RestApi.Services;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api")]
    public class MoveController : ApiController
    {
        private readonly IMoveStatService _moveStatService;
        private readonly ICharacterStatService _characterStatService;

        public MoveController(IMoveStatService moveStatService, ICharacterStatService characterStatService)
        {
            _moveStatService = moveStatService;
            _characterStatService = characterStatService;
        }

        [Authorize(Roles = "Basic")]
        [Route("movesoftype/{type}")]
        [HttpGet]
        public IHttpActionResult GetMovesOfType(MoveType type)
        {
            var moves = from move in _moveStatService.GetMovesByType(type)
                        select new MoveDTO(move, _characterStatService);

            return Ok(moves);
        }

        [Authorize(Roles = "Basic")]
        [Route("moves")]
        [HttpGet]
        public IHttpActionResult GetMovesOfName([FromUri]string name)
        {
            var moves = from move in _moveStatService.GetMovesByName(name)
                        select new MoveDTO(move, _characterStatService);

            return Ok(moves);
        }

        [Authorize(Roles = "Basic")]
        [Route("moves/{id}")]
        [HttpGet]
        public IHttpActionResult GetMove(int id)
        {
            var move = _moveStatService.GetMove(id);
            var moveDTO = new MoveDTO(move, _characterStatService);
            return Ok(moveDTO);
        }

        [Authorize(Roles = "Admin")]
        [Route("moves")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]MoveStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundMove = _moveStatService.GetMove(value.Id);

            if (foundMove == null)
            {
                _moveStatService.CreateMove(value);
            }
            else
            {
                return BadRequest();
            }

            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [Route("moves/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]MoveStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            var foundMove = _moveStatService.GetMove(value.Id);
            if (foundMove != null)
            {
                foundMove.Angle = value.Angle;
                foundMove.AutoCancel = value.AutoCancel;
                foundMove.BaseDamage = value.BaseDamage;
                foundMove.BaseKnockBackSetKnockback = value.BaseKnockBackSetKnockback;
                foundMove.FirstActionableFrame = value.FirstActionableFrame;
                foundMove.HitboxActive = value.HitboxActive;
                foundMove.KnockbackGrowth = value.KnockbackGrowth;
                foundMove.LandingLag = value.LandingLag;
                foundMove.Name = value.Name;
                foundMove.TotalHitboxActiveLength = value.TotalHitboxActiveLength;
                foundMove.Type = value.Type;

                _moveStatService.UpdateMove(value);

                return Ok(value);
            }


            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "Admin")]
        [Route("moves/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var move = _moveStatService.GetMove(id);
            if (move == null)
            {
                return NotFound();
            }

            _moveStatService.DeleteMove(move);

            return Ok();
        }
    }
}
