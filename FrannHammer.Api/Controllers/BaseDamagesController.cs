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
    /// Handles operations dealing with <see cref="BaseDamage"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class BaseDamagesController : BaseApiController
    {
        private const string BaseDamagesRouteKey = "BaseDamages";

        /// <summary>
        /// Create a new <see cref="FrannHammer.Api.Controllers.BaseDamagesController"/> to interact with the server using 
        /// a specific <see cref="IApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public BaseDamagesController(IApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="BaseDamageDto"/>s.
        /// </summary>
        /// <returns></returns>
        [Route(BaseDamagesRouteKey)]
        public IQueryable<BaseDamageDto> GetBaseDamages()
        {
            var baseDamageTypes = Db.BaseDamage.ProjectTo<BaseDamageDto>();
            return baseDamageTypes;
        }

        /// <summary>
        /// Get a specific <see cref="BaseDamageDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(BaseDamageDto))]
        [Route(BaseDamagesRouteKey + "/{id}")]
        public IHttpActionResult GetBaseDamage(int id)
        {
            var dto = (from baseDamage in Db.BaseDamage
                       join moves in Db.Moves
                           on baseDamage.MoveId equals moves.Id
                       where baseDamage.Id == id
                       select baseDamage).ProjectTo<BaseDamageDto>()
                       .SingleOrDefault();

            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="BaseDamageDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(BaseDamagesRouteKey + "/{id}")]
        public IHttpActionResult PutBaseDamage(int id, BaseDamageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!BaseDamageExists(id))
            {
                return NotFound();
            }

            var entity = Db.BaseDamage.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="BaseDamageDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(BaseDamageDto))]
        [Route(BaseDamagesRouteKey)]
        public IHttpActionResult PostBaseDamage(BaseDamageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<BaseDamageDto, BaseDamage>(dto);
            entity.LastModified = DateTime.Now;

            Db.BaseDamage.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<BaseDamage, BaseDamageDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "BaseDamages", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="BaseDamage"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(BaseDamagesRouteKey + "/{id}")]
        public IHttpActionResult DeleteBaseDamage(int id)
        {
            BaseDamage baseDamage = Db.BaseDamage.Find(id);
            if (baseDamage == null)
            {
                return NotFound();
            }

            Db.BaseDamage.Remove(baseDamage);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        private bool BaseDamageExists(int id)
        {
            return Db.BaseDamage.Count(e => e.Id == id) > 0;
        }
    }
}