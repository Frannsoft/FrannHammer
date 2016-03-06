using Kurogane.Data.RestApi.DTOs;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Net;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;

namespace Kurogane.Data.RestApi.Controllers
{
    public class MovementController : ApiController
    {
        private readonly IMovementStatService _movementStatService;
        private readonly ICharacterStatService _characterStatService;

        public MovementController(IMovementStatService movementStatService, ICharacterStatService characterStatService)
        {
            _movementStatService = movementStatService;
            _characterStatService = characterStatService;
        }

        [Authorize(Roles = "Admin")]
        [Route("movements")]
        [HttpGet]
        public IHttpActionResult GetMovements()
        {
            var movementsResult = from movements in _movementStatService.GetMovementStats()
                   select new MovementStatDto(movements, _characterStatService);
            return Ok(movementsResult);
        }

        [Route("movement/{id}")]
        [HttpGet]
        public IHttpActionResult GetMovementStat(int id)
        {
            var movement = _movementStatService.GetMovementStat(id);
            var movementDto = new MovementStatDto(movement, _characterStatService);
            return Ok(movementDto);
        }

        [Route("movement")]
        [HttpGet]
        public IEnumerable<MovementStatDto> GetAllMovementOfName([FromUri]string name)
        {
            return from movements in _movementStatService.GetMovementStatsByName(name)
                   select new MovementStatDto(movements, _characterStatService);
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

            var foundMovement = _movementStatService.GetMovementStat(value.Id);
            
            if(foundMovement == null)
            {
                _movementStatService.CreateMovementStat(value);
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

            var foundMovement = _movementStatService.GetMovementStat(value.Id);
            if (foundMovement != null)
            {
                foundMovement.Name = value.Name;
                foundMovement.Value = value.Value;

                _movementStatService.UpdateMovementStat(foundMovement);
                return Ok(value);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        [Route("movement/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var movement = _movementStatService.GetMovementStat(id);
            if (movement == null)
            {
                return NotFound();
            }

            _movementStatService.DeleteMovementStat(movement);

            return Ok();
        }
    }
}
