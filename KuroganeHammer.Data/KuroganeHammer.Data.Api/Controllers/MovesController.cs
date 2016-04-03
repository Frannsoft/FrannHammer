using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KuroganeHammer.Data.Api.Models;
using static KuroganeHammer.Data.Api.Models.RolesConstants;
using System;

namespace KuroganeHammer.Data.Api.Controllers
{
    [RoutePrefix("api")]
    public class MovesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public MovesController()
        { }

        public MovesController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: api/Moves
        [Authorize(Roles = Basic)]
        public IQueryable<Move> GetMoves()
        {
            return db.Moves;
        }

        // GET: api/Moves/5
        [Authorize(Roles = Basic)]
        [ResponseType(typeof(Move))]
        public IHttpActionResult GetMove(int id)
        {
            Move move = db.Moves.Find(id);
            if (move == null)
            {
                return NotFound();
            }

            return Ok(move);
        }

        // PUT: api/Moves/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMove(int id, Move move)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != move.Id)
            {
                return BadRequest();
            }

            move.LastModified = DateTime.Now;
            db.Entry(move).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoveExists(id))
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

        // POST: api/Moves
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(Move))]
        public IHttpActionResult PostMove(Move move)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            move.LastModified = DateTime.Now;
            db.Moves.Add(move);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = move.Id }, move);
        }

        // DELETE: api/Moves/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(Move))]
        public IHttpActionResult DeleteMove(int id)
        {
            Move move = db.Moves.Find(id);
            if (move == null)
            {
                return NotFound();
            }

            db.Moves.Remove(move);
            db.SaveChanges();

            return Ok(move);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoveExists(int id)
        {
            return db.Moves.Count(e => e.Id == id) > 0;
        }
    }
}