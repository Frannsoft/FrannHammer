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
    /// Handles server operations dealing with <see cref="Movement"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class MovementsController : BaseApiController
    {
        public MovementsController(ApplicationDbContext context)
            : base(context)
        {
        }

        //Query too big to be useful.
        /// <summary>
        /// Get all movement data.
        /// </summary>
        [ResponseType(typeof(IQueryable<Movement>))]
        [Route("movements", Name = "GetAllMovements")]
        public IQueryable<Movement> GetMovements()
        {
            return Db.Movements;
        }

        /// <summary>
        /// Get all of the <see cref="Movement"/> data that is a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<MovementDto>))]
        [Route("movements/byname", Name = "GetMovementsByName")]
        public IQueryable<MovementDto> GetMovementsByName([FromUri] string name)
        {
            var movements = Db.Movements.Where(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ProjectTo<MovementDto>();
            return movements;
        }

        /// <summary>
        /// Get a specific <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(MovementDto))]
        [Route("movements/{id}")]
        public IHttpActionResult GetMovement(int id)
        {
            Movement movement = Db.Movements.Find(id);
            if (movement == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<Movement, MovementDto>(movement);

            return Ok(dto);
        }

        /// <summary>
        /// Update a specific <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [Route("movements/{id}")]
        public IHttpActionResult PutMovement(int id, MovementDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!MovementExists(id))
            {
                return NotFound();
            }

            var entity = Db.Movements.Find(id);

            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="Movement"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(MovementDto))]
        [Route("movements")]
        public IHttpActionResult PostMovement(MovementDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<MovementDto, Movement>(dto);
            entity.LastModified = DateTime.Now;
            Db.Movements.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<Movement, MovementDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "Movements", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route("movements/{id}")]
        public IHttpActionResult DeleteMovement(int id)
        {
            Movement movement = Db.Movements.Find(id);
            if (movement == null)
            {
                return NotFound();
            }

            Db.Movements.Remove(movement);
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

        private bool MovementExists(int id)
        {
            return Db.Movements.Count(e => e.Id == id) > 0;
        }
    }
}