using Kurogane.Data.RestApi.DTOs;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using KuroganeHammer.Service;
using KuroganeHammer.Model;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace Kurogane.Data.RestApi.Controllers
{
    public class MovementController : ApiController
    {
        private readonly IMovementStatService movementStatService;
        private readonly ICharacterStatService characterStatService;

        public MovementController(IMovementStatService movementStatService, ICharacterStatService characterStatService)
        {
            this.movementStatService = movementStatService;
            this.characterStatService = characterStatService;
        }

        [Route("frannhammerAPI/movement/{id}")]
        [HttpGet]
        public IHttpActionResult GetMovementStat(int id)
        {
            var movement = movementStatService.GetMovementStat(id);
            var movementDTO = new MovementStatDTO(movement, characterStatService);
            return Ok(movementDTO);
        }

        [Route("frannhammerAPI/movement")]
        [HttpGet]
        public IEnumerable<MovementStatDTO> GetAllMovementOfName([FromUri]string name)
        {
            return from movements in movementStatService.GetMovementStatsByName(name)
                   select new MovementStatDTO(movements, characterStatService);
        }

        [Route("frannhammerAPI/movement")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]MovementStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            movementStatService.CreateMovementStat(value);

            return Ok(value);
        }

        [Route("frannhammerAPI/movements/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]MovementStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            movementStatService.UpdateMovementStat(value);

            try
            {
                movementStatService.SaveMovement();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovementStatExists(id))
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

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            MovementStat movement = movementStatService.GetMovementStat(id);
            if (movement == null)
            {
                return NotFound();
            }

            movementStatService.DeleteMovementStat(movement);

            return Ok();
        }

        private bool MovementStatExists(int id)
        {
            return movementStatService.GetMovementStat(id) != null;
        }
    }
}
