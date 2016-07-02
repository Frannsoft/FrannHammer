using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.Models;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles the more broad types that can be assigned to other metadata in the Db.
    /// </summary>
    [RoutePrefix("api")]
    public class SmashAttributeTypesController : BaseApiController
    {
        public SmashAttributeTypesController(ApplicationDbContext context)
            : base(context)
        { }

        /// <summary>
        /// Get all of the stored <see cref="SmashAttributeType"/>s.
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IQueryable<SmashAttributeType>))]
        [Route("smashattributetypes")]
        public IQueryable<SmashAttributeType> GetSmashAttributeTypes()
        {
            return Db.SmashAttributeTypes;
        }

        /// <summary>
        /// Get a specific <see cref="SmashAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(SmashAttributeType))]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult GetSmashAttributeType(int id)
        {
            SmashAttributeType smashAttributeType = Db.SmashAttributeTypes.Find(id);
            if (smashAttributeType == null)
            {
                return NotFound();
            }

            return Ok(smashAttributeType);
        }

        /// <summary>
        /// Get back all of the <see cref="SmashAttributeType"/>s of a specific id
        /// as <see cref="CharacterAttribute"/> objects.  This call parses the returned 
        /// <see cref="CharacterAttribute"/>s into <see cref="CharacterAttributeRowDto"/>, 
        /// similar to how they are displayed on KuroganeHammer.com.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("smashattributetypes/{id}/characterattributes")]
        public IHttpActionResult GetAllCharacterAttributeOfSmashAttributeType(int id)
        {
            SmashAttributeType smashAttributeType = Db.SmashAttributeTypes.Find(id);
            if (smashAttributeType == null)
            {
                return NotFound();
            }

            //create a 'row' from each pulled back characterattribute since a characterattribute only represents
            //a single cell of a row in the existing KH site.
            var characterAttributeRows =
                Db.CharacterAttributes.Where(c => c.SmashAttributeType.Id == smashAttributeType.Id)
                    .ToList() //execute query and bring into memory so I can continue to query against the data below
                    .GroupBy(a => a.OwnerId)
                    .Select(g => new CharacterAttributeRowDto(g.First().Rank, smashAttributeType.Id,
                        g.Key, g.Select(at => new CharacterAttributeKeyValuePair()
                        {
                            KeyName = at.Name,
                            ValueCharacterAttribute = new CharacterAttribute
                            {
                                Id = at.Id,
                                Name = at.Name,
                                OwnerId = at.OwnerId,
                                Rank = at.Rank,
                                SmashAttributeType = new SmashAttributeType
                                {
                                    Id = at.SmashAttributeType.Id,
                                    Name = at.Name
                                },
                                SmashAttributeTypeId = at.SmashAttributeTypeId,
                                Value = at.Value
                            }
                        }).ToList(),
                        Db.Characters.Find(g.Key).Name, Db.Characters.Find(g.Key).ThumbnailUrl))
                        .ToList();

            return Ok(characterAttributeRows);
        }

        /// <summary>
        /// Update an existing <see cref="SmashAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="smashAttributeType"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult PutSmashAttributeType(int id, SmashAttributeType smashAttributeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != smashAttributeType.Id)
            {
                return BadRequest();
            }

            smashAttributeType.LastModified = DateTime.Now;
            Db.Entry(smashAttributeType).State = EntityState.Modified;

            try
            {
                Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmashAttributeTypeExists(id))
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
        /// Create a new <see cref="SmashAttributeType"/>.
        /// </summary>
        /// <param name="smashAttributeType"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(SmashAttributeType))]
        [Route("smashattributetypes")]
        public IHttpActionResult PostSmashAttributeType(SmashAttributeType smashAttributeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            smashAttributeType.LastModified = DateTime.Now;
            Db.SmashAttributeTypes.Add(smashAttributeType);
            Db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { controller = "SmashAttributeTypes", id = smashAttributeType.Id }, smashAttributeType);
        }

        /// <summary>
        /// Delete an existing <see cref="SmashAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(SmashAttributeType))]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult DeleteSmashAttributeType(int id)
        {
            SmashAttributeType smashAttributeType = Db.SmashAttributeTypes.Find(id);
            if (smashAttributeType == null)
            {
                return NotFound();
            }

            Db.SmashAttributeTypes.Remove(smashAttributeType);
            Db.SaveChanges();

            return Ok(smashAttributeType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SmashAttributeTypeExists(int id)
        {
            return Db.SmashAttributeTypes.Count(e => e.Id == id) > 0;
        }
    }
}