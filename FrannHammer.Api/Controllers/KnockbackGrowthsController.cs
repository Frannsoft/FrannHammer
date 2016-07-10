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
    /// Handles operations dealing with <see cref="KnockbackGrowth"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class KnockbackGrowthsController : BaseApiController
    {
        private const string KnockbackGrowthsRouteKey = "KnockbackGrowths";

        /// <summary>
        /// Create a new <see cref="KnockbackGrowthsController"/> to interact with the server using 
        /// a specific <see cref="IApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public KnockbackGrowthsController(IApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="KnockbackGrowthDto"/>s.
        /// </summary>
        /// <returns></returns>
        [Route(KnockbackGrowthsRouteKey)]
        public IQueryable<KnockbackGrowthDto> GetKnockbackGrowths()
        {
            var knockbackGrowthTypes = Db.KnockbackGrowth.ProjectTo<KnockbackGrowthDto>();
            return knockbackGrowthTypes;
        }

        /// <summary>
        /// Get a specific <see cref="KnockbackGrowthDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(KnockbackGrowthDto))]
        [Route(KnockbackGrowthsRouteKey + "/{id}")]
        public IHttpActionResult GetKnockbackGrowth(int id)
        {
            var dto = (from knockbackGrowth in Db.KnockbackGrowth
                       join moves in Db.Moves
                           on knockbackGrowth.MoveId equals moves.Id
                       where knockbackGrowth.Id == id
                       select knockbackGrowth).ProjectTo<KnockbackGrowthDto>()
                      .SingleOrDefault();

            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="KnockbackGrowthDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(KnockbackGrowthsRouteKey + "/{id}")]
        public IHttpActionResult PutKnockbackGrowth(int id, KnockbackGrowthDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!KnockbackGrowthExists(id))
            {
                return NotFound();
            }

            var entity = Db.KnockbackGrowth.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="KnockbackGrowthDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(KnockbackGrowthDto))]
        [Route(KnockbackGrowthsRouteKey)]
        public IHttpActionResult PostKnockbackGrowth(KnockbackGrowthDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<KnockbackGrowthDto, KnockbackGrowth>(dto);
            entity.LastModified = DateTime.Now;

            Db.KnockbackGrowth.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<KnockbackGrowth, KnockbackGrowthDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "KnockbackGrowths", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="KnockbackGrowth"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(KnockbackGrowthsRouteKey + "/{id}")]
        public IHttpActionResult DeleteKnockbackGrowth(int id)
        {
            KnockbackGrowth knockbackGrowth = Db.KnockbackGrowth.Find(id);
            if (knockbackGrowth == null)
            {
                return NotFound();
            }

            Db.KnockbackGrowth.Remove(knockbackGrowth);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        private bool KnockbackGrowthExists(int id)
        {
            return Db.KnockbackGrowth.Count(e => e.Id == id) > 0;
        }
    }
}