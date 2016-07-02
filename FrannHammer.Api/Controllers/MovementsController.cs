using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.Models;
using FrannHammer.Models;

namespace FrannHammer.Api.Controllers
{

    [RoutePrefix("api")]
    public class MovementsController : BaseApiController
    {
        public MovementsController(ApplicationDbContext context)
            : base(context)
        {
        }

        //Query too big to be useful.
        ///// <summary>
        ///// Get all movement data.
        ///// </summary>
        //[ResponseType(typeof(IQueryable<Movement>))]
        //[Route("movements", Name = "GetAllMovements")]
        //public IQueryable<Movement> GetMovements()
        //{
        //    return Db.Movements;

        //    return (from movement in Db.Movements.ToList()
        //            select new MovementDto(movement,
        //            Db.Characters.First(c => c.Id == movement.OwnerId))
        //        ).AsQueryable();
        //}

        /// <summary>
        /// Get all of the <see cref="Movement"/> data that is a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<Movement>))]
        [Route("movements/byname", Name = "GetMovementsByName")]
        public IQueryable<Movement> GetMovementsByName([FromUri] string name)
        {
            return Db.Movements.Where(m => m.Name.Equals(name));
        }

        /// <summary>
        /// Get a specific <see cref="Movement"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Movement))]
        [Route("movements/{id}")]
        public IHttpActionResult GetMovement(int id)
        {
            Movement movement = Db.Movements.Find(id);
            if (movement == null)
            {
                return NotFound();
            }

            return Ok(movement);
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
            Db.Entry(movement).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
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
            Db.Movements.Add(movement);
            Db.SaveChanges();

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
            Movement movement = Db.Movements.Find(id);
            if (movement == null)
            {
                return NotFound();
            }

            Db.Movements.Remove(movement);
            Db.SaveChanges();

            return Ok(movement);
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