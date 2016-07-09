using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FrannHammer.Api.Models;
using FrannHammer.Models;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles server operations dealing with <see cref="Character"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class CharactersController : BaseApiController
    {
        private const string CharactersRouteKey = "Characters";

        /// <summary>
        /// Create a new <see cref="CharactersController"/> to interact with the server using 
        /// a specific <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public CharactersController(IApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all of the <see cref="CharacterDto"/> details.
        /// </summary>
        /// <returns></returns>
        [Route(CharactersRouteKey)]
        public IQueryable<CharacterDto> GetCharacters()
        {
            return Db.Characters.ProjectTo<CharacterDto>();
        }

        /// <summary>
        /// Get a specific <see cref="CharacterDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterDto))]
        [Route(CharactersRouteKey + "/{id}")]
        public IHttpActionResult GetCharacter(int id)
        {
            //TODO: error check for invalid param to give better feedback
            var character = Db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }
            var dto = Mapper.Map<Character, CharacterDto>(character);

            return Ok(dto);
        }

        /// <summary>
        /// Get a specific <see cref="CharacterDto"/>s details by their name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterDto))]
        [Route(CharactersRouteKey + "/name/{name}")]
        public IHttpActionResult GetCharacterByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            { return BadRequest($"Parameter {nameof(name)} cannot be empty."); }

            Character character = Db.Characters.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<Character, CharacterDto>(character);

            return Ok(dto);
        }

        /// <summary>
        /// Get all the <see cref="Movement"/> data for a specific <see cref="Character"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(CharactersRouteKey + "/{id}/movements")]
        [HttpGet]
        public IQueryable<MovementDto> GetMovementsForCharacter(int id)
        {
            var movements = Db.Movements.Where(m => m.OwnerId == id).ProjectTo<MovementDto>();
            return movements;
        }

        /// <summary>
        /// Get all the <see cref="Move"/> data for a specific <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(CharactersRouteKey + "/{id}/moves")]
        public IQueryable<MoveDto> GetMovesForCharacter(int id)
        {
            var moves = Db.Moves.Where(m => m.OwnerId == id).ProjectTo<MoveDto>();
            return moves;
        }

        /// <summary>
        /// Get all the <see cref="Throw"/>s for the specific <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<ThrowDto>))]
        [Route(CharactersRouteKey + "/{id}/throws")]
        public IQueryable<ThrowDto> GetThrowsForCharacter(int id)
        {
            return (from throws in Db.Throws
                    join moves in Db.Moves
                        on throws.MoveId equals moves.Id
                    where moves.OwnerId == id
                    select throws).ProjectTo<ThrowDto>();
        }

        /// <summary>
        /// Get all the <see cref="CharacterAttribute"/>s of a specific <see cref="Character"/>.
        /// 
        /// These will be returned as <see cref="CharacterAttributeDto"/>s.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(CharactersRouteKey + "/{id}/characterattributes")]
        [ResponseType(typeof(IQueryable<CharacterAttributeDto>))]
        public IQueryable<CharacterAttributeDto> GetCharacterAttributesForCharacter(int id)
        {
            var attrs = Db.CharacterAttributes.Where(a => a.OwnerId == id).ProjectTo<CharacterAttributeDto>();
            return attrs;
        }

        /// <summary>
        /// Get all the <see cref="Angle"/>s of a specific <see cref="Character"/>'s moves.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(CharactersRouteKey + "/{id}/angles")]
        [ResponseType(typeof (IQueryable<AngleDto>))]
        public IQueryable<AngleDto> GetCharacterMoveAngles(int id)
        {
            return (from angle in Db.Angle
                join moves in Db.Moves
                    on angle.MoveId equals moves.Id
                where moves.OwnerId == id
                select angle).ProjectTo<AngleDto>();
        }

        /// <summary>
        /// Get all the <see cref="Hitbox"/>es of a specific <see cref="Character"/>'s moves.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(CharactersRouteKey + "/{id}/hitboxes")]
        [ResponseType(typeof (IQueryable<HitboxDto>))]
        public IQueryable<HitboxDto> GetCharacterMoveHitboxes(int id)
        {
            return (from hitbox in Db.Hitbox
                    join moves in Db.Moves
                        on hitbox.MoveId equals moves.Id
                    where moves.OwnerId == id
                    select hitbox).ProjectTo<HitboxDto>();
        }

        /// <summary>
        /// Get all the <see cref="KnockbackGrowth"/>es of a specific <see cref="Character"/>'s moves.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(CharactersRouteKey + "/{id}/knockbackgrowths")]
        [ResponseType(typeof(IQueryable<KnockbackGrowthDto>))]
        public IQueryable<KnockbackGrowthDto> GetCharacterMoveKnockbackGrowths(int id)
        {
            return (from knockbackGrowth in Db.KnockbackGrowth
                    join moves in Db.Moves
                        on knockbackGrowth.MoveId equals moves.Id
                    where moves.OwnerId == id
                    select knockbackGrowth).ProjectTo<KnockbackGrowthDto>();
        }

        /// <summary>
        /// Get all the <see cref="BaseDamage"/>s of a specific <see cref="Character"/>'s moves.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(CharactersRouteKey + "/{id}/basedamages")]
        [ResponseType(typeof(IQueryable<BaseDamageDto>))]
        public IQueryable<BaseDamageDto> GetCharacterMoveBaseDamages(int id)
        {
            return (from baseDamage in Db.BaseDamage
                    join moves in Db.Moves
                        on baseDamage.MoveId equals moves.Id
                    where moves.OwnerId == id
                    select baseDamage).ProjectTo<BaseDamageDto>();
        }

        /// <summary>
        /// Update a <see cref="CharacterDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [Route(CharactersRouteKey + "/{id}")]
        public IHttpActionResult PutCharacter(int id, CharacterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!CharacterExists(id))
            {
                return NotFound();
            }

            var entity = Db.Characters.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="CharacterDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(CharacterDto))]
        [Route(CharactersRouteKey + "")]
        public IHttpActionResult PostCharacter(CharacterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<CharacterDto, Character>(dto);
            entity.LastModified = DateTime.Now;
            Db.Characters.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<Character, CharacterDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = CharactersRouteKey + "", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Character"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(CharactersRouteKey + "/{id}")]
        public IHttpActionResult DeleteCharacter(int id)
        {
            Character character = Db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            Db.Characters.Remove(character);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        private bool CharacterExists(int id)
        {
            return Db.Characters.Count(e => e.Id == id) > 0;
        }
    }
}