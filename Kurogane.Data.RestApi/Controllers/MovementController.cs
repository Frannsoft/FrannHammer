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

        [Route("movement/{id}")]
        [HttpGet]
        public IHttpActionResult GetMovementStat(int id)
        {
            var movement = movementStatService.GetMovementStat(id);
            var movementDTO = new MovementStatDTO(movement, characterStatService);
            return Ok(movementDTO);
        }

        [Route("movement")]
        [HttpGet]
        public IEnumerable<MovementStatDTO> GetAllMovementOfName([FromUri]string name)
        {
            return from movements in movementStatService.GetMovementStatsByName(name)
                   select new MovementStatDTO(movements, characterStatService);
        }

        [Authorize]
        [Route("movement")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]MovementStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundMovement = movementStatService.GetMovementStat(value.Id);
            
            if(foundMovement == null)
            {
                movementStatService.CreateMovementStat(value);
            }
            else
            {
                return BadRequest();
            }

            return Ok(value);
        }

        [Authorize]
        [Route("movement/{id}")]
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

            var foundMovement = movementStatService.GetMovementStat(value.Id);
            if (foundMovement != null)
            {
                foundMovement.Name = value.Name;
                foundMovement.Value = value.Value;

                movementStatService.UpdateMovementStat(foundMovement);
                return Ok(value);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        [Route("movement/{id}")]
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
