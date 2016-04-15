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
    /// <summary>
    /// Handles all individual <see cref="CharacterAttribute"/> operations.  
    /// </summary>
    [RoutePrefix("api")]
    public class CharacterAttributesController : BaseApiController
    {

        /// <summary>
        /// Create a new <see cref="CharacterAttribute"/> controller to interact with the server.
        /// </summary>
        public CharacterAttributesController()
        { }

        /// <summary>
        /// Create a new <see cref="CharacterAttribute"/> controller to interact with the server using a specific 
        /// <see cref="ApplicationDbContext"/>.
        /// </summary>
        public CharacterAttributesController(ApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all <see cref="CharacterAttribute"/>s.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Basic)]
        [Route("characterattributes")]
        public IQueryable<CharacterAttribute> GetCharacterAttributes()
        {
            return Db.CharacterAttributes;
        }

        /// <summary>
        /// Get a specific <see cref="CharacterAttribute"/> by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Basic)]
        [ResponseType(typeof(CharacterAttribute))]
        [Route("characterattributes/{id}")]
        public IHttpActionResult GetCharacterAttribute(int id)
        {
            CharacterAttribute characterAttribute = Db.CharacterAttributes.Find(id);
            if (characterAttribute == null)
            {
                return NotFound();
            }

            return Ok(characterAttribute);
        }

        /// <summary>
        /// Update a <see cref="CharacterAttribute"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="characterAttribute"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(void))]
        [Route("characterattributes/{id}")]
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

            Db.Entry(characterAttribute).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
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

        /// <summary>
        /// Create a new <see cref="CharacterAttribute"/>.
        /// </summary>
        /// <param name="characterAttribute"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [Route("characterattributes")]
        public IHttpActionResult PostCharacterAttribute(CharacterAttribute characterAttribute)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            characterAttribute.LastModified = DateTime.Now;
            Db.CharacterAttributes.Add(characterAttribute);
            Db.SaveChanges();

            var result = CreatedAtRoute("DefaultApi", new { controller="CharacterAttributes", id = characterAttribute.Id }, characterAttribute);
            return result;
        }

        /// <summary>
        /// Delete a <see cref="CharacterAttribute"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(CharacterAttribute))]
        [Route("characterattributes/{id}")]
        public IHttpActionResult DeleteCharacterAttribute(int id)
        {
            CharacterAttribute characterAttribute = Db.CharacterAttributes.Find(id);
            if (characterAttribute == null)
            {
                return NotFound();
            }

            Db.CharacterAttributes.Remove(characterAttribute);
            Db.SaveChanges();

            return Ok(characterAttribute);
        }

        private bool CharacterAttributeExists(int id)
        {
            return Db.CharacterAttributes.Count(e => e.Id == id) > 0;
        }
    }
}