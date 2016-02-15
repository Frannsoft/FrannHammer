using Kurogane.Data.RestApi.DTOs;
using System.Web.Http;
using System.Linq;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Providers;

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

        [Authorize]
        [Route("characters")]
        [HttpGet]
        public IHttpActionResult GetRoster()
        {
            var characterDTOs = from characters in characterStatService.GetCharacters()
                                orderby characters.Name ascending
                                select new CharacterDTO(characters);

            return Ok(characterDTOs);
        }

        [Route("characters/{id}")]
        [HttpGet]
        public IHttpActionResult GetCharacter(int id)
        {
            var character = characterStatService.GetCharacter(id);
            CharacterDTO charDTO = new CharacterDTO(character);
            return Ok(character);
        }

        [Route("characters/{id}/movement")]
        [HttpGet]
        public IHttpActionResult GetMovementForRoster(int id)
        {
            var movementStats = from movements in movementStatService.GetMovementStatsForCharacter(id)
                                select new MovementStatDTO(movements, characterStatService);
            return Ok(movementStats);
        }

        [Route("characters/{id}/moves")]
        [HttpGet]
        public IHttpActionResult GetMoves(int id)
        {
            var moves = from move in moveStatService.GetMovesByCharacter(id)
                        where move.OwnerId == id
                        select new MoveDTO(move, characterStatService);

            return Ok(moves);
        }

        [Route("characters/{id}/moves/{type}")]
        [HttpGet]
        public IHttpActionResult GetMoveForCharacterOfType(int id, MoveType type)
        {
            var moves = from move in moveStatService.GetMovesByCharacter(id)
                        where move.OwnerId == id &&
                        move.Type == type
                        select new MoveDTO(move, characterStatService);

            return Ok(moves);
        }

        [Authorize]
        [Route("characters")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CharacterStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundChar = characterStatService.GetCharacter(value.Id);
            if (foundChar == null)
            {
                characterStatService.CreateCharacter(value);
            }
            else
            {
                return BadRequest();
            }

            return Ok(value);
        }

        [Authorize]
        [Route("characters/{id}")]
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

            var foundChar = characterStatService.GetCharacter(value.Id);
            if (foundChar != null)
            {
                foundChar.Description = value.Description;
                foundChar.MainImageUrl = value.MainImageUrl;
                foundChar.Name = value.Name;
                foundChar.Style = value.Style;
                foundChar.ThumbnailUrl = value.ThumbnailUrl;

                characterStatService.UpdateCharacter(foundChar);
            }

            return Ok(value);
        }

        [Authorize]
        [Route("characters/{id}")]
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
