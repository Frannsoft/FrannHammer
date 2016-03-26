using Kurogane.Data.RestApi.DTOs;
using System.Web.Http;
using System.Linq;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;

namespace Kurogane.Data.RestApi.Controllers
{
    [RoutePrefix("api")]
    public class CharacterController : ApiController
    {
        private readonly ICharacterStatService _characterStatService;
        private readonly IMovementStatService _movementStatService;
        private readonly IMoveStatService _moveStatService;
        private readonly ICharacterAttributeService _characterAttributeService;

        public CharacterController(ICharacterStatService characterStatService, IMovementStatService movementStatService, IMoveStatService moveStatService,
            ICharacterAttributeService characterAttributeService)
        {
            _characterStatService = characterStatService;
            _movementStatService = movementStatService;
            _moveStatService = moveStatService;
            _characterAttributeService = characterAttributeService;
        }

        [Authorize(Roles = "Admin, Basic")]
        [Route("characters")]
        [HttpGet]
        public IHttpActionResult GetRoster()
        {
            var characterDtOs = from characters in _characterStatService.GetCharacters()
                                orderby characters.Name ascending
                                select new CharacterDTO(characters);

            return Ok(characterDtOs);
        }

        [Authorize(Roles = "Basic")]
        [Route("characters/{id}")]
        [HttpGet]
        public IHttpActionResult GetCharacter(int id)
        {
            var character = _characterStatService.GetCharacter(id);
            var charDto = new CharacterDTO(character);
            return Ok(charDto);
        }

        [Authorize(Roles = "Basic")]
        [Route("characters/{id}/movement")]
        [HttpGet]
        public IHttpActionResult GetMovementForRoster(int id)
        {
            var movementStats = from movements in _movementStatService.GetMovementStatsForCharacter(id)
                                select new MovementStatDTO(movements, _characterStatService);
            return Ok(movementStats);
        }

        [Authorize(Roles = "Basic")]
        [Route("characters/{id}/moves")]
        [HttpGet]
        public IHttpActionResult GetMoves(int id)
        {
            var moves = from move in _moveStatService.GetMovesByCharacter(id)
                        where move.OwnerId == id
                        select new MoveDTO(move, _characterStatService);

            return Ok(moves);
        }

        [Authorize(Roles = "Basic")]
        [Route("characters/{id}/attributes")]
        [HttpGet]
        public IHttpActionResult GetAttributesForCharacter(int id)
        {
            var attributes = from attribute in _characterAttributeService.GetCharacterAttributesByCharacter(id)
                             where attribute.OwnerId == id
                             select new CharacterAttributeDTO(attribute);

            return Ok(attributes);
        }

        //[Route("characters/{id}/moves/{type}")]
        //[HttpGet]
        //public IHttpActionResult GetMoveForCharacterOfType(int id, MoveType type)
        //{
        //    var moves = from move in _moveStatService.GetMovesByCharacter(id)
        //                where move.OwnerId == id &&
        //                move.Type == type
        //                select new MoveDTO(move, _characterStatService);

        //    return Ok(moves);
        //}

        [Authorize(Roles = "Admin")]
        [Route("characters")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]CharacterStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundChar = _characterStatService.GetCharacter(value.Id);
            if (foundChar == null)
            {
                _characterStatService.CreateCharacter(value);
            }
            else
            {
                return BadRequest();
            }

            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [Route("characters/{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CharacterStat value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != value.OwnerId)
            {
                return BadRequest();
            }

            var foundChar = _characterStatService.GetCharacter(value.OwnerId);
            if (foundChar != null)
            {
                foundChar.Description = value.Description;
                foundChar.MainImageUrl = value.MainImageUrl;
                foundChar.Name = value.Name;
                foundChar.Style = value.Style;
                foundChar.ThumbnailUrl = value.ThumbnailUrl;

                _characterStatService.UpdateCharacter(foundChar);
            }

            return Ok(value);
        }

        [Authorize(Roles = "Admin")]
        [Route("characters/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var character = _characterStatService.GetCharacter(id);
            if (character == null)
            {
                return NotFound();
            }

            _characterStatService.DeleteCharacter(character);

            return Ok();
        }
    }
}
