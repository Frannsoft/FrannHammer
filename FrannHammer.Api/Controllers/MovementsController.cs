using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.DTOs;
using FrannHammer.Api.Models;
using FrannHammer.Core.Models;

namespace FrannHammer.Api.Controllers
{
    
    [RoutePrefix("api")]
    public class MovementsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public MovementsController()
        { }

        public MovementsController(ApplicationDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get all movement data.
        /// </summary>
        [ResponseType(typeof(IQueryable<MovementDto>))]
        [Route("movements", Name = "GetAllMovements")]
        public IQueryable<MovementDto> GetMovements()
        {
            return (from movement in db.Movements.ToList()
                    select new MovementDto(movement,
                    db.Characters.First(c => c.Id == movement.OwnerId))
                ).AsQueryable();
        }

        /// <summary>
        /// Get all of the <see cref="MovementDto"/> data that is a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<MovementDto>))]
        [Route("movements/byname", Name = "GetMovementsByName")]
        public IQueryable<MovementDto> GetMovementsByName([FromUri] string name)
        {
            return (from movement in db.Movements.Where(m => m.Name.Equals(name)).ToList()
                    select new MovementDto(movement,
                    db.Characters.First(c => c.Id == movement.OwnerId))
                ).AsQueryable();
        }

        /// <summary>
        /// Get a specific <see cref="MovementDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(MovementDto))]
        [Route("movements/{id}")]
        public IHttpActionResult GetMovement(int id)
        {
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return NotFound();
            }

            var dto = new MovementDto(movement,
                db.Characters.First(c => c.Id == movement.OwnerId));
            return Ok(dto);
        }

        /// <summary>
        /// Update a specific <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movement"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [Route("movements/{id}")]
        public IHttpActionResult PutMovement(int id, Movement movement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movement.Id)
            {
                return BadRequest();
            }

            movement.LastModified = DateTime.Now;
            db.Entry(movement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="Movement"/>.
        /// </summary>
        /// <param name="movement"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Movement))]
        [Route("movements")]
        public IHttpActionResult PostMovement(Movement movement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            movement.LastModified = DateTime.Now;
            db.Movements.Add(movement);
            db.SaveChanges();

            //var dto = new MovementDto(movement,
            //    db.Characters.First(c => c.Id == movement.OwnerId));
            return CreatedAtRoute("DefaultApi", new { controller = "Movements", id = movement.Id }, movement);
        }

        /// <summary>
        /// Delete a <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Movement))]
        [Route("movements/{id}")]
        public IHttpActionResult DeleteMovement(int id)
        {
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return NotFound();
            }

            db.Movements.Remove(movement);
            db.SaveChanges();

            return Ok(movement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovementExists(int id)
        {
            return db.Movements.Count(e => e.Id == id) > 0;
        }
    }
}