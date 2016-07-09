using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FrannHammer.Api.Models;
using FrannHammer.Models;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles all individual <see cref="CharacterAttribute"/> operations.  
    /// </summary>
    [RoutePrefix("api")]
    public class CharacterAttributesController : BaseApiController
    {

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
        [Route("characterattributes")]
        internal IQueryable<CharacterAttributeDto> GetCharacterAttributes()
        {
            return Db.CharacterAttributes.ProjectTo<CharacterAttributeDto>();
        }

        /// <summary>
        /// Get a specific <see cref="CharacterAttribute"/> by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeDto))]
        [Route("characterattributes/{id}")]
        public IHttpActionResult GetCharacterAttribute(int id)
        {
            CharacterAttribute characterAttribute = Db.CharacterAttributes.Find(id);
            if (characterAttribute == null)
            {
                return NotFound();
            }
            var dto = Mapper.Map<CharacterAttribute, CharacterAttributeDto>(characterAttribute);

            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="CharacterAttribute"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [Route("characterattributes/{id}")]
        public IHttpActionResult PutCharacterAttribute(int id, CharacterAttributeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!CharacterAttributeExists(id))
            {
                return NotFound();
            }

            var entity = Db.CharacterAttributes.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="CharacterAttribute"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route("characterattributes")]
        public IHttpActionResult PostCharacterAttribute(CharacterAttributeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<CharacterAttributeDto, CharacterAttribute>(dto);
            entity.LastModified = DateTime.Now;
            Db.CharacterAttributes.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<CharacterAttribute, CharacterAttributeDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "CharacterAttributes", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="CharacterAttribute"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
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

            return StatusCode(HttpStatusCode.OK);
        }

        private bool CharacterAttributeExists(int id)
        {
            return Db.CharacterAttributes.Count(e => e.Id == id) > 0;
        }
    }
}