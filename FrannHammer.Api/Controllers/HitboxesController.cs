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
        private const string HitboxsRouteKey = "Hitboxes";

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
        [Route(HitboxsRouteKey)]
        public IQueryable<HitboxDto> GetHitboxs()
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
        [Route(HitboxsRouteKey + "/{id}")]
        public IHttpActionResult GetHitbox(int id)
        {
            Hitbox retHitbox = Db.Hitbox.Find(id);
            if (retHitbox == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<Hitbox, HitboxDto>(retHitbox);
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="HitboxDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(HitboxsRouteKey + "/{id}")]
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
        [Route(HitboxsRouteKey)]
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
        [Route(HitboxsRouteKey + "/{id}")]
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