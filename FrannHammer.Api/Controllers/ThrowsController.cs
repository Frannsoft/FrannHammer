using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using FrannHammer.Api.Models;
using FrannHammer.Models;
using AutoMapper.QueryableExtensions;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles operations dealing with <see cref="Throw"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class ThrowsController : BaseApiController
    {
        private const string ThrowsRouteKey = "Throws";

        /// <summary>
        /// Create a new <see cref="ThrowsController"/> to interact with the server using 
        /// a specific <see cref="IApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public ThrowsController(IApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="ThrowDto"/>s.
        /// </summary>
        /// <returns></returns>
        [Route(ThrowsRouteKey)]
        public IQueryable<ThrowDto> GetThrows()
        {
            var throwTypes = Db.Throws.ProjectTo<ThrowDto>();
            return throwTypes;
        }

        /// <summary>
        /// Get a specific <see cref="ThrowDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(ThrowDto))]
        [Route(ThrowsRouteKey + "/{id}")]
        public IHttpActionResult GetThrow(int id)
        {
            Throw retThrow = Db.Throws.Find(id);
            if (retThrow == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<Throw, ThrowDto>(retThrow);
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="ThrowDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(ThrowsRouteKey + "/{id}")]
        public IHttpActionResult PutThrow(int id, ThrowDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!ThrowExists(id))
            {
                return NotFound();
            }

            var entity = Db.Throws.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="ThrowDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(ThrowDto))]
        [Route(ThrowsRouteKey)]
        public IHttpActionResult PostThrow(ThrowDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<ThrowDto, Throw>(dto);
            entity.LastModified = DateTime.Now;

            Db.Throws.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<Throw, ThrowDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "Throws", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Throw"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(ThrowsRouteKey + "/{id}")]
        public IHttpActionResult DeleteThrow(int id)
        {
            Throw Throw = Db.Throws.Find(id);
            if (Throw == null)
            {
                return NotFound();
            }

            Db.Throws.Remove(Throw);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        private bool ThrowExists(int id)
        {
            return Db.Throws.Count(e => e.Id == id) > 0;
        }
    }
}