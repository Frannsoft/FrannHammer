using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.Models;
using FrannHammer.Core.Models;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles operations dealing with <see cref="Notation"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class NotationsController : BaseApiController
    {
        /// <summary>
        /// Create a new <see cref="NotationsController"/> to interact with the server using 
        /// a specific <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public NotationsController(ApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="Notation"/>s.
        /// </summary>
        /// <returns></returns>
        [Route("notations")]
        public IQueryable<Notation> GetNotations()
        {
            return Db.Notations;
        }

        /// <summary>
        /// Get a specific <see cref="Notation"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Notation))]
        [Route("notations/{id}")]
        public IHttpActionResult GetNotation(int id)
        {
            Notation notation = Db.Notations.Find(id);
            if (notation == null)
            {
                return NotFound();
            }

            return Ok(notation);
        }

        /// <summary>
        /// Update a <see cref="Notation"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notation"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route("notations/{id}")]
        public IHttpActionResult PutNotation(int id, Notation notation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notation.Id)
            {
                return BadRequest();
            }

            notation.LastModified = DateTime.Now;
            Db.Entry(notation).State = System.Data.Entity.EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotationExists(id))
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
        /// Create a new <see cref="Notation"/>.
        /// </summary>
        /// <param name="notation"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Notation))]
        [Route("notations")]
        public IHttpActionResult PostNotation(Notation notation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Db.Notations.Add(notation);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "Notations", id = notation.Id }, notation);
        }

        /// <summary>
        /// Delete a <see cref="Notation"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Notation))]
        [Route("notations/{id}")]
        public IHttpActionResult DeleteNotation(int id)
        {
            Notation notation = Db.Notations.Find(id);
            if (notation == null)
            {
                return NotFound();
            }

            Db.Notations.Remove(notation);
            Db.SaveChanges();

            return Ok(notation);
        }

        private bool NotationExists(int id)
        {
            return Db.Notations.Count(e => e.Id == id) > 0;
        }
    }
}