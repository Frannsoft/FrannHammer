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
    public class MovesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public MovesController()
        { }

        public MovesController(ApplicationDbContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get all moves.  Not sure if this is sticking around.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<MoveDto>))]
        [Route("moves")]
        public IQueryable<MoveDto> GetMoves()
        {
            return (from move in db.Moves.ToList()
                    select new MoveDto(move,
                        db.Characters.Find(move.OwnerId))// .First(c => c.Id == move.OwnerId))
                ).AsQueryable();
        }

        /// <summary>
        /// Get all moves that have a specific name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<MoveDto>))]
        [Route("moves/byname")]
        public IQueryable<MoveDto> GetMovesByName([FromUri] string name)
        {
            return (from move in db.Moves.Where(m => m.Name.Equals(name)).ToList()
                    select new MoveDto(move,
                        db.Characters.First(c => c.Id == move.OwnerId))
                ).AsQueryable();
        }

        /// <summary>
        /// Get a specific <see cref="MoveDto"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(MoveDto))]
        [Route("moves/{id}")]
        public IHttpActionResult GetMove(int id)
        {
            Move move = db.Moves.Find(id);
            if (move == null)
            {
                return NotFound();
            }

            var dto = new MoveDto(move,
                db.Characters.First(c => c.Id == move.OwnerId));
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="move"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(IHttpActionResult))]
        [Route("moves/{id}")]
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

        /// <summary>
        /// Create a new <see cref="Move"/>.
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Move))]
        [Route("moves")]
        public IHttpActionResult PostMove(Move move)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            move.LastModified = DateTime.Now;
            db.Moves.Add(move);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "Moves", id = move.Id }, move);
        }

        /// <summary>
        /// Delete a <see cref="Move"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(Move))]
        [Route("moves/{id}")]
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