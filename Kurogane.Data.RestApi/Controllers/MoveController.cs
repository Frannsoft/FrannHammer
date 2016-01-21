using Kurogane.Data.RestApi.DTOs;
using KuroganeHammer.Model;
using KuroganeHammer.Service;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Kurogane.Data.RestApi.Controllers
{
    public class MoveController : ApiController
    {
        private readonly IMoveStatService moveStatService;
        private readonly ICharacterStatService characterStatService;

        public MoveController(IMoveStatService moveStatService, ICharacterStatService characterStatService)
        {
            this.moveStatService = moveStatService;
            this.characterStatService = characterStatService;
        }

        [Route("movesoftype/{type}")]
        [HttpGet]
        public IHttpActionResult GetMovesOfType(MoveType type)
        {
            var moves = from move in moveStatService.GetMovesByType(type)
                        select new MoveDTO(move, characterStatService);

            return Ok(moves);
        }

        [Route("movesofname")]
        [HttpGet]
        public IHttpActionResult GetMovesOfName([FromUri]string name)
        {
            var moves = from move in moveStatService.GetMovesByName(name)
                        select new MoveDTO(move, characterStatService);

            return Ok(moves);
        }

        [Route("moves/{id}")]
        [HttpGet]
        public IHttpActionResult GetMove(int id)
        {
            var move = moveStatService.GetMove(id);
            return Ok(move);
        }

        [Route("moves")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]MoveStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundMove = moveStatService.GetMove(value.Id);

            if(foundMove == null)
            {
                moveStatService.CreateMove(value);
            }
            else
            {
                return BadRequest();
            }

            return Ok(value);
        }

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

            var foundMove = moveStatService.GetMove(value.Id);
            if(foundMove != null)
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

                moveStatService.UpdateMove(value);

                return Ok(value);
            }


            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("moves/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            MoveStat move = moveStatService.GetMove(id);
            if (move == null)
            {
                return NotFound();
            }

            moveStatService.DeleteMove(move);

            return Ok();
        }

        private bool MoveStatExists(int id)
        {
            return moveStatService.GetMove(id) != null;
        }
    }
}
