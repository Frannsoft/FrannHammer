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
    /// Handles server operations dealing with <see cref="Move"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class MovesController : BaseApiController
    {
        private const string MovesRouteKey = "moves";

        public MovesController(ApplicationDbContext context)
            : base(context)
        { }

        //Too big to be useful
        /// <summary>
        /// Get all moves.  Not sure if this is sticking around.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<MoveDto>))]
        [Route(MovesRouteKey)]
        public IQueryable<MoveDto> GetMoves()
        {
            return Db.Moves.ProjectTo<MoveDto>();
        }

        /// <summary>
        /// Get all moves that have a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<MoveDto>))]
        [Route(MovesRouteKey + "/byname")]
        public IQueryable<MoveDto> GetMovesByName([FromUri] string name)
        {
            return Db.Moves.Where(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ProjectTo<MoveDto>();
        }

        /// <summary>
        /// Get the <see cref="Hitbox"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(MovesRouteKey + "/{id}/hitboxes")]
        public IHttpActionResult GetMoveHitboxData(int id)
        {
            var entity = Db.Hitbox.SingleOrDefault(h => h.MoveId == id);

            if (entity == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<Hitbox, HitboxDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Get the <see cref="Angle"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(MovesRouteKey + "/{id}/angles")]
        public IHttpActionResult GetMoveAngleData(int id)
        {
            var entity = Db.Angle.SingleOrDefault(a => a.MoveId == id);

            if (entity == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<Angle, AngleDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Get the <see cref="BaseDamage"/> data associated with this move.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(MovesRouteKey + "/{id}/basedamages")]
        public IHttpActionResult GetMoveBaseDamageData(int id)
        {
            var entity = Db.BaseDamage.SingleOrDefault(b => b.MoveId == id);

            if (entity == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<BaseDamage, BaseDamageDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Get the <see cref="KnockbackGrowth"/> data associated with this <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route(MovesRouteKey + "/{id}/knockbackgrowths")]
        public IHttpActionResult GetMoveKnockbackGrowthData(int id)
        {
            var entity = Db.KnockbackGrowth.SingleOrDefault(b => b.MoveId == id);

            if (entity == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<KnockbackGrowth, KnockbackGrowthDto>(entity);
            return Ok(dto);
        }

        /// <summary>
        /// Get a specific <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [Route(MovesRouteKey + "/{id}")]
        public IHttpActionResult GetMove(int id)
        {
            Move move = Db.Moves.Find(id);
            if (move == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<Move, MoveDto>(move);
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(IHttpActionResult))]
        [Route(MovesRouteKey + "/{id}")]
        public IHttpActionResult PutMove(int id, MoveDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!MoveExists(id))
            {
                return NotFound();
            }

            var entity = Db.Moves.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="Move"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(MoveDto))]
        [Route(MovesRouteKey + "")]
        public IHttpActionResult PostMove(MoveDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<MoveDto, Move>(dto);
            entity.LastModified = DateTime.Now;
            Db.Moves.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<Move, MoveDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = MovesRouteKey + "", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Move))]
        [Route(MovesRouteKey + "/{id}")]
        public IHttpActionResult DeleteMove(int id)
        {
            Move move = Db.Moves.Find(id);
            if (move == null)
            {
                return NotFound();
            }

            Db.Moves.Remove(move);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoveExists(int id)
        {
            return Db.Moves.Count(e => e.Id == id) > 0;
        }
    }
}