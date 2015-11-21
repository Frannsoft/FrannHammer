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

        [Route("frannhammerAPI/movesoftype/{type}")]
        [HttpGet]
        public IHttpActionResult GetMovesOfType(MoveType type)
        {
            var moves = from move in moveStatService.GetMovesByType(type)
                        select new MoveDTO(move, characterStatService);

            return Ok(moves);
        }

        [Route("frannhammerAPI/movesofname")]
        [HttpGet]
        public IHttpActionResult GetMovesOfName([FromUri]string name)
        {
            var moves = from move in moveStatService.GetMovesByName(name)
                        select new MoveDTO(move, characterStatService);

            return Ok(moves);
        }

        [Route("frannhammerAPI/moves/{id}")]
        [HttpGet]
        public IHttpActionResult GetMove(int id)
        {
            var move = moveStatService.GetMove(id);
            return Ok(move);
        }

        [Route("frannhammerAPI/move")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]MoveStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            moveStatService.CreateMove(value);

            return Ok(value);
        }

        [Route("frannhammerAPI/move/{id}")]
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

            moveStatService.UpdateMove(value);

            try
            {
                moveStatService.SaveMove();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoveStatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("frannhammerAPI/move")]
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
