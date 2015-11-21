using Kurogane.Data.RestApi.DTOs;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using KuroganeHammer.Service;
using KuroganeHammer.Model;
using System.Data.Entity.Infrastructure;
using System.Net;
using KuroganeHammer.Data.Infrastructure;

namespace Kurogane.Data.RestApi.Controllers
{
    public class CharacterController : ApiController
    {
        private readonly ICharacterStatService characterStatService;
        private readonly IMovementStatService movementStatService;
        private readonly IMoveStatService moveStatService;

        public CharacterController(ICharacterStatService characterStatService, IMovementStatService movementStatService, IMoveStatService moveStatService)
        {
            this.characterStatService = characterStatService;
            this.movementStatService = movementStatService;
            this.moveStatService = moveStatService;
        }

        [Route("frannhammerAPI/characters")]
        [HttpGet]
        public IHttpActionResult GetRoster()
        {
            var characterDTOs = from characters in characterStatService.GetCharacters()
                                select new CharacterDTO(characters, characterStatService);

            return Ok<IEnumerable<CharacterDTO>>(characterDTOs);
        }

        [Route("frannhammerAPI/characters/{id}")]
        [HttpGet]
        public IHttpActionResult GetCharacter(int id)
        {
            var character = characterStatService.GetCharacter(id);
            CharacterDTO charDTO = new CharacterDTO(character, characterStatService);
            return Ok(character);
        }

        [Route("frannhammerAPI/characters/{id}/movement")]
        [HttpGet]
        public IHttpActionResult GetMovementForRoster(int id)
        {
            var movementStats = from movements in movementStatService.GetMovementStatsForCharacter(id)
                                select new MovementStatDTO(movements, characterStatService);
            return Ok(movementStats);
        }

        [Route("frannhammerAPI/characters/{id}/moves")]
        [HttpGet]
        public IHttpActionResult GetMoves(int id)
        {
            var moves = from move in moveStatService.GetMovesByCharacter(id)
                        where move.OwnerId == id
                        select new MoveDTO(move, characterStatService);

            return Ok(moves);
        }

        [Route("frannhammerAPI/characters/{id}/moves/{type}")]
        [HttpGet]
        public IHttpActionResult GetMoveForCharacterOfType(int id, MoveType type)
        {
            var moves = from move in moveStatService.GetMovesByCharacter(id)
                        where move.OwnerId == id &&
                        move.Type == type
                        select new MoveDTO(move, characterStatService);

            return Ok(moves);
        }

        [Route("frannhammerAPI/characters")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CharacterStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            characterStatService.CreateCharacter(value);

            return Ok(value);
        }

        [Route("frannhammerAPI/characters/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CharacterStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.Id)
            {
                return BadRequest();
            }

            characterStatService.UpdateCharacter(value);

            try
            {
                characterStatService.SaveCharacter();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        [Route("frannhammerAPI/characters/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            CharacterStat character = characterStatService.GetCharacter(id);
            if (character == null)
            {
                return NotFound();
            }

            characterStatService.DeleteCharacter(character);

            return Ok();
        }

        private bool CharacterExists(int id)
        {
            return characterStatService.GetCharacter(id) != null;
        }
    }
}
