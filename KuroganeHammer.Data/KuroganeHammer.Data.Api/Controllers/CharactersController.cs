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
    public class CharactersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CharactersController()
        { }

        public CharactersController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: api/Characters
        [Authorize(Roles = Basic)]
        [Route("characters")]
        public IQueryable<Character> GetCharacters()
        {
            return db.Characters;
        }

        // GET: api/Characters/5
        [Authorize(Roles = Basic)]
        [ResponseType(typeof(Character))]
        [Route("characters/{id}")]
        public IHttpActionResult GetCharacter(int id)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [Authorize(Roles = Basic)]
        [Route("Characters/{id}/movements")]
        [HttpGet]
        public IQueryable<Movement> GetMovementsForCharacter(int id)
        {
            return db.Movements.Where(m => m.OwnerId == id);
        }

        [Authorize(Roles = Basic)]
        [Route("Characters/{id}/moves")]
        public IQueryable<Move> GetMovesForCharacter(int id)
        {
            return db.Moves.Where(m => m.OwnerId == id);
        }

        // PUT: api/Characters/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(void))]
        [Route("characters/{id}")]
        public IHttpActionResult PutCharacter(int id, Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != character.Id)
            {
                return BadRequest();
            }

            character.LastModified = DateTime.Now;
            db.Entry(character).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/Characters
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(Character))]
        [Route("characters")]
        public IHttpActionResult PostCharacter(Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Characters.Add(character);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "Characters", id = character.Id }, character);
        }

        // DELETE: api/Characters/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(Character))]
        [Route("characters/{id}")]
        public IHttpActionResult DeleteCharacter(int id)
        {
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return NotFound();
            }

            db.Characters.Remove(character);
            db.SaveChanges();

            return Ok(character);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CharacterExists(int id)
        {
            return db.Characters.Count(e => e.Id == id) > 0;
        }
    }
}