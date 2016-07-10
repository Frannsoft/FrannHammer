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
    /// Handles operations dealing with <see cref="Angle"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class AnglesController : BaseApiController
    {
        private const string AnglesRouteKey = "Angles";

        /// <summary>
        /// Create a new <see cref="AnglesController"/> to interact with the server using 
        /// a specific <see cref="IApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public AnglesController(IApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="AngleDto"/>s.
        /// </summary>
        /// <returns></returns>
        [Route(AnglesRouteKey)]
        public IQueryable<AngleDto> GetAngles()
        {
            var angleTypes = Db.Angle.ProjectTo<AngleDto>();
            return angleTypes;
        }

        /// <summary>
        /// Get a specific <see cref="AngleDto"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(AngleDto))]
        [Route(AnglesRouteKey + "/{id}")]
        public IHttpActionResult GetAngle(int id)
        {
            var dto = (from angle in Db.Angle
                join moves in Db.Moves
                    on angle.MoveId equals moves.Id
                where angle.Id == id
                select angle).ProjectTo<AngleDto>().SingleOrDefault();

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="AngleDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Route(AnglesRouteKey + "/{id}")]
        public IHttpActionResult PutAngle(int id, AngleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!AngleExists(id))
            {
                return NotFound();
            }

            var entity = Db.Angle.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="AngleDto"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(AngleDto))]
        [Route(AnglesRouteKey)]
        public IHttpActionResult PostAngle(AngleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<AngleDto, Angle>(dto);
            entity.LastModified = DateTime.Now;

            Db.Angle.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<Angle, AngleDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "Angles", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Angle"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(AnglesRouteKey + "/{id}")]
        public IHttpActionResult DeleteAngle(int id)
        {
            Angle angle = Db.Angle.Find(id);
            if (angle == null)
            {
                return NotFound();
            }

            Db.Angle.Remove(angle);
            Db.SaveChanges();

            return StatusCode(HttpStatusCode.OK);
        }

        private bool AngleExists(int id)
        {
            return Db.Angle.Count(e => e.Id == id) > 0;
        }
    }
}