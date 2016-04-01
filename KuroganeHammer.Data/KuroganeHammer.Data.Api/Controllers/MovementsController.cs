﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KuroganeHammer.Data.Api.Models;

namespace KuroganeHammer.Data.Api.Controllers
{
    public class MovementsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Movements
        public IQueryable<Movement> GetMovements()
        {
            return db.Movements;
        }

        // GET: api/Movements/5
        [ResponseType(typeof(Movement))]
        public IHttpActionResult GetMovement(int id)
        {
            Movement movement = db.Movements.Find(id);
            if (movement == null)
            {
                return NotFound();
            }

            return Ok(movement);
        }

        // PUT: api/Movements/5
        [ResponseType(typeof(void))]
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

        // POST: api/Movements
        [ResponseType(typeof(Movement))]
        public IHttpActionResult PostMovement(Movement movement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movements.Add(movement);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movement.Id }, movement);
        }

        // DELETE: api/Movements/5
        [ResponseType(typeof(Movement))]
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