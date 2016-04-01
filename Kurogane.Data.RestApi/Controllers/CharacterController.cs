using System;
using Kurogane.Data.RestApi.DTOs;
using System.Web.Http;
using System.Linq;
using Kurogane.Data.RestApi.Models;
using Kurogane.Data.RestApi.Services;
using static Kurogane.Data.RestApi.Models.RolesConstants;

namespace Kurogane.Data.RestApi.Controllers
{
    public class CharacterController : ApiController
    {
        private readonly ICharacterStatService _characterStatService;
        private readonly IMovementStatService _movementStatService;
        private readonly IMoveStatService _moveStatService;
        private readonly ICharacterAttributeService _characterAttributeService;

        public CharacterController(ICharacterStatService characterStatService, IMovementStatService movementStatService, 
            IMoveStatService moveStatService,
            ICharacterAttributeService characterAttributeService)
        {
            _characterStatService = characterStatService;
            _movementStatService = movementStatService;
            _moveStatService = moveStatService;
            _characterAttributeService = characterAttributeService;
        }

        [Authorize(Roles = Basic)]
        [Route("characters")]
        [HttpGet]
        public IHttpActionResult GetCharacters()
        {
            //var characterDtOs = from characters in _characterStatService.GetCharacters()
            //                    orderby characters.Name ascending
            //                    select new CharacterDTO(characters);

            //return Ok(characterDtOs);

            var characters = _characterStatService.GetCharacters();

            return Ok(characters);
        }

        [Authorize(Roles = Basic)]
        [Route("characters/{id}")]
        [HttpGet]
        public IHttpActionResult GetCharacter(int id)
        {
            var character = _characterStatService.GetCharacter(id);
            var charDto = new CharacterDTO(character);
            return Ok(character);
        }

        [Authorize(Roles = Basic)]
        [Route("characters/{id}/movement")]
        [HttpGet]
        public IHttpActionResult GetMovementForRoster(int id)
        {
            var movementStats = from movements in _movementStatService.GetMovementStatsForCharacter(id)
                                select new MovementStatDTO(movements, _characterStatService);
            return Ok(movementStats);
        }

        [Authorize(Roles = Basic)]
        [Route("characters/{id}/moves")]
        [HttpGet]
        public IHttpActionResult GetMoves(int id)
        {
            var moves = from move in _moveStatService.GetMovesByCharacter(id)
                        where move.OwnerId == id
                        select new MoveDTO(move, _characterStatService);

            return Ok(moves);
        }

        [Authorize(Roles = Basic)]
        [Route("characters/{id}/attributes")]
        [HttpGet]
        public IHttpActionResult GetAttributesForCharacter(int id)
        {
            var attributes = _characterAttributeService.GetCharacterAttributesByCharacter(id)
                .GroupBy(a => a.OwnerId)
                .Select(g => new CharacterAttributeRowDTO(g.First().Rank, g.First().SmashAttributeTypeId, g.Key, g.ToDictionary(at => at.Name, at => at), _characterStatService));

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

        [Authorize(Roles = Admin)]
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

            return CreatedAtRoute("DefaultApi", new {controller = "Character", id = value.Id}, value);
        }

        [Authorize(Roles = Admin)]
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

            var foundChar = _characterStatService.GetCharacter(value.Id);
            if (foundChar != null)
            {
                foundChar.Description = value.Description;
                foundChar.MainImageUrl = value.MainImageUrl;
                foundChar.Name = value.Name;
                foundChar.Style = value.Style;
                foundChar.ThumbnailUrl = value.ThumbnailUrl;
                foundChar.LastModified = DateTime.Now;

                _characterStatService.UpdateCharacter(foundChar);
            }

            return Ok(value);
        }

        [Authorize(Roles = Admin)]
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
