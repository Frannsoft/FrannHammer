using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.Models;
using FrannHammer.Models;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles operations dealing with <see cref="Throw"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class ThrowsController : BaseApiController
    {
        const string ThrowsRouteKey = "Throws";

        /// <summary>
        /// Create a new <see cref="ThrowsController"/> to interact with the server using 
        /// a specific <see cref="IApplicationDbContext"/>
        /// </summary>
        /// <param name="context"></param>
        public ThrowsController(IApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="Throw"/>s.
        /// </summary>
        /// <returns></returns>
        [Route(ThrowsRouteKey)]
        public IQueryable<Throw> GetThrows()
        {
            return Db.Throws;
        }

        /// <summary>
        /// Get a specific <see cref="Throw"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Throw))]
        [Route(ThrowsRouteKey + "/{id}")]
        public IHttpActionResult GetThrow(int id)
        {
            Throw Throw = Db.Throws.Find(id);
            if (Throw == null)
            {
                return NotFound();
            }

            return Ok(Throw);
        }

        /// <summary>
        /// Update a <see cref="Throw"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Throw"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [Route(ThrowsRouteKey + "/{id}")]
        public IHttpActionResult PutThrow(int id, Throw Throw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Throw.Id)
            {
                return BadRequest();
            }

            Throw.LastModified = DateTime.Now;
            Db.Entry(Throw).State = System.Data.Entity.EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThrowExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="Throw"/>.
        /// </summary>
        /// <param name="Throw"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Throw))]
        [Route(ThrowsRouteKey)]
        public IHttpActionResult PostThrow(Throw Throw)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Db.Throws.Add(Throw);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "Throws", id = Throw.Id }, Throw);
        }

        /// <summary>
        /// Delete a <see cref="Throw"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Throw))]
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

            return Ok(Throw);
        }

        private bool ThrowExists(int id)
        {
            return Db.Throws.Count(e => e.Id == id) > 0;
        }
    }
}