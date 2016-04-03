using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using static KuroganeHammer.Data.Api.Models.RolesConstants;
using KuroganeHammer.Data.Api.Models;

namespace KuroganeHammer.Data.Api.Controllers
{
    [RoutePrefix("api")]
    public class CharacterAttributesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public CharacterAttributesController()
        { }

        public CharacterAttributesController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: api/CharacterAttributes
        [Authorize(Roles = Basic)]
        public IQueryable<CharacterAttribute> GetCharacterAttributes()
        {
            return db.CharacterAttributes;
        }

        // GET: api/CharacterAttributes/5
        [Authorize(Roles = Basic)]
        [ResponseType(typeof(CharacterAttribute))]
        public IHttpActionResult GetCharacterAttribute(int id)
        {
            CharacterAttribute characterAttribute = db.CharacterAttributes.Find(id);
            if (characterAttribute == null)
            {
                return NotFound();
            }

            return Ok(characterAttribute);
        }

        // PUT: api/CharacterAttributes/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCharacterAttribute(int id, CharacterAttribute characterAttribute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            characterAttribute.LastModified = DateTime.Now;
            if (id != characterAttribute.Id)
            {
                return BadRequest();
            }

            db.Entry(characterAttribute).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterAttributeExists(id))
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

        // POST: api/CharacterAttributes
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(CharacterAttribute))]
        public IHttpActionResult PostCharacterAttribute(CharacterAttribute characterAttribute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            characterAttribute.LastModified = DateTime.Now;
            db.CharacterAttributes.Add(characterAttribute);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = characterAttribute.Id }, characterAttribute);
        }

        // DELETE: api/CharacterAttributes/5
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(CharacterAttribute))]
        public IHttpActionResult DeleteCharacterAttribute(int id)
        {
            CharacterAttribute characterAttribute = db.CharacterAttributes.Find(id);
            if (characterAttribute == null)
            {
                return NotFound();
            }

            db.CharacterAttributes.Remove(characterAttribute);
            db.SaveChanges();

            return Ok(characterAttribute);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CharacterAttributeExists(int id)
        {
            return db.CharacterAttributes.Count(e => e.Id == id) > 0;
        }
    }
}