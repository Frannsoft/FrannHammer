using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using KuroganeHammer.Data.Api.Models;
using KuroganeHammer.Data.Core.Models;
using static KuroganeHammer.Data.Api.Models.RolesConstants;

namespace KuroganeHammer.Data.Api.Controllers
{
    /// <summary>
    /// Handles the backing types of <see cref="CharacterAttribute"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class CharacterAttributeTypesController : BaseApiController
    {
        /// <summary>
        /// Create a new <see cref="CharacterAttributeTypesController"/>.
        /// </summary>
        public CharacterAttributeTypesController()
        { }

        /// <summary>
        /// Create a new <see cref="CharacterAttributeTypesController"/> using
        /// a specific <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="context"></param>
        public CharacterAttributeTypesController(ApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all of the <see cref="CharacterAttributeType"/> details.
        /// </summary>
        /// <returns></returns>
        [Route("characterattributetypes")]
        public IQueryable<CharacterAttributeType> GetCharacterAttributeTypes()
        {
            return Db.CharacterAttributeTypes;
        }

        /// <summary>
        /// Get a specific <see cref="CharacterAttributeType"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeType))]
        [Route("characterattributetypes/{id}")]
        public IHttpActionResult GetCharacterAttributeType(int id)
        {
            CharacterAttributeType characterAttributeType = Db.CharacterAttributeTypes.Find(id);
            if (characterAttributeType == null)
            {
                return NotFound();
            }

            return Ok(characterAttributeType);
        }

        /// <summary>
        /// Update a <see cref="CharacterAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="characterAttributeType"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(void))]
        [Route("characterattributetypes/{id}")]
        public IHttpActionResult PutCharacterAttributeType(int id, CharacterAttributeType characterAttributeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != characterAttributeType.Id)
            {
                return BadRequest();
            }

            characterAttributeType.LastModified = DateTime.Now;
            Db.Entry(characterAttributeType).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterAttributeTypeExists(id))
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
        /// Create a new <see cref="CharacterAttributeType"/>.
        /// </summary>
        /// <param name="characterAttributeType"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(CharacterAttributeType))]
        [Route("characterattributetypes")]
        public IHttpActionResult PostCharacterAttributeType(CharacterAttributeType characterAttributeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Db.CharacterAttributeTypes.Add(characterAttributeType);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "CharacterAttributeTypes", id = characterAttributeType.Id }, characterAttributeType);
        }

        /// <summary>
        /// Delete a <see cref="CharacterAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = Admin)]
        [ResponseType(typeof(CharacterAttributeType))]
        [Route("characterattributetypes/{id}")]
        public IHttpActionResult DeleteCharacterAttributeType(int id)
        {
            CharacterAttributeType characterAttributeType = Db.CharacterAttributeTypes.Find(id);
            if (characterAttributeType == null)
            {
                return NotFound();
            }

            Db.CharacterAttributeTypes.Remove(characterAttributeType);
            Db.SaveChanges();

            return Ok(characterAttributeType);
        }

        private bool CharacterAttributeTypeExists(int id)
        {
            return Db.CharacterAttributeTypes.Count(e => e.Id == id) > 0;
        }
    }
}