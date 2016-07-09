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
    /// Handles operations dealing with <see cref="ThrowType"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class ThrowTypesController : BaseApiController
    {
        private const string ThrowTypesRouteKey = "ThrowTypes";

        /// <summary>
        /// Create a new <see cref="ThrowTypesController"/> to interact with the server using 
        /// a specific <see cref="IApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public ThrowTypesController(IApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="Throw"/>s.
        /// </summary>
        /// <returns></returns>
        [Route(ThrowTypesRouteKey)]
        public IQueryable<ThrowTypeDto> GetThrowTypes()
        {
            var throwTypes = Db.ThrowTypes.ProjectTo<ThrowTypeDto>();
            return throwTypes;
        }

        /// <summary>
        /// Get a specific <see cref="Throw"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(ThrowTypeDto))]
        [Route(ThrowTypesRouteKey + "/{id}")]
        public IHttpActionResult GetThrowType(int id)
        {
            ThrowType type = Db.ThrowTypes.Find(id);
            if (type == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<ThrowType, ThrowTypeDto>(type);

            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="ThrowType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="throwType"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route(ThrowTypesRouteKey + "/{id}")]
        public IHttpActionResult PutThrowType(int id, ThrowTypeDto throwType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != throwType.Id)
            {
                return BadRequest();
            }

            if (!ThrowTypeExists(id))
            {
                return NotFound();
            }

            var entity = Db.ThrowTypes.Find(id);
            entity = Mapper.Map(throwType, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="ThrowType"/>.
        /// </summary>
        /// <param name="throwTypeDto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(ThrowTypeDto))]
        [Route(ThrowTypesRouteKey)]
        public IHttpActionResult PostThrowType(ThrowTypeDto throwTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<ThrowTypeDto, ThrowType>(throwTypeDto);

            entity.LastModified = DateTime.Now;
            Db.ThrowTypes.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<ThrowType, ThrowTypeDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "ThrowTypes", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="ThrowTypeDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(ThrowTypesRouteKey + "/{id}")]
        public IHttpActionResult DeleteThrowType(int id)
        {
            ThrowType type = Db.ThrowTypes.Find(id);
            if (type == null)
            {
                return NotFound();
            }

            Db.ThrowTypes.Remove(type);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        private bool ThrowTypeExists(int id)
        {
            return Db.ThrowTypes.Count(e => e.Id == id) > 0;
        }
    }
}