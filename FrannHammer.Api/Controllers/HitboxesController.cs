using System;
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
    /// Handles operations dealing with <see cref="Hitbox"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class HitboxesController : BaseApiController
    {
        private const string HitboxesRouteKey = "Hitboxes";

        /// <summary>
        /// Create a new <see cref="HitboxesController"/> to interact with the server using 
        /// a specific <see cref="IApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public HitboxesController(IApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="HitboxDto"/>s.
        /// </summary>
        /// <returns></returns>
        [Route(HitboxesRouteKey)]
        public IQueryable<HitboxDto> GetHitboxes()
        {
            var hitboxes = Db.Hitbox.ProjectTo<HitboxDto>();
            return hitboxes;
        }

        /// <summary>
        /// Get a specific <see cref="HitboxDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(HitboxDto))]
        [Route(HitboxesRouteKey + "/{id}")]
        public IHttpActionResult GetHitbox(int id)
        {
            var dto = (from hitbox in Db.Hitbox
                       join moves in Db.Moves
                           on hitbox.MoveId equals moves.Id
                       where hitbox.Id == id
                       select hitbox).ProjectTo<HitboxDto>()
                       .SingleOrDefault();

            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="HitboxDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(HitboxesRouteKey + "/{id}")]
        public IHttpActionResult PutHitbox(int id, HitboxDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!HitboxExists(id))
            {
                return NotFound();
            }

            var entity = Db.Hitbox.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="HitboxDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(HitboxDto))]
        [Route(HitboxesRouteKey)]
        public IHttpActionResult PostHitbox(HitboxDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<HitboxDto, Hitbox>(dto);
            entity.LastModified = DateTime.Now;

            Db.Hitbox.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<Hitbox, HitboxDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "Hitboxes", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Hitbox"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(HitboxesRouteKey + "/{id}")]
        public IHttpActionResult DeleteHitbox(int id)
        {
            Hitbox hitbox = Db.Hitbox.Find(id);
            if (hitbox == null)
            {
                return NotFound();
            }

            Db.Hitbox.Remove(hitbox);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        private bool HitboxExists(int id)
        {
            return Db.Hitbox.Count(e => e.Id == id) > 0;
        }
    }
}