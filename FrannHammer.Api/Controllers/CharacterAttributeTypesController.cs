using System;
using System.Data.Entity;
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
    /// Handles the backing types of <see cref="CharacterAttribute"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class CharacterAttributeTypesController : BaseApiController
    {
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
        public IQueryable<CharacterAttributeTypeDto> GetCharacterAttributeTypes()
        {
            return Db.CharacterAttributeTypes.ProjectTo<CharacterAttributeTypeDto>();
        }

        /// <summary>
        /// Get a specific <see cref="CharacterAttributeType"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeTypeDto))]
        [Route("characterattributetypes/{id}")]
        public IHttpActionResult GetCharacterAttributeType(int id)
        {
            CharacterAttributeType characterAttributeType = Db.CharacterAttributeTypes.Find(id);
            if (characterAttributeType == null)
            {
                return NotFound();
            }

            var dto = Mapper.Map<CharacterAttributeType, CharacterAttributeTypeDto>(characterAttributeType);
            return Ok(dto);
        }

        /// <summary>
        /// Update a <see cref="CharacterAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [Route("characterattributetypes/{id}")]
        public IHttpActionResult PutCharacterAttributeType(int id, CharacterAttributeTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            if (!CharacterAttributeTypeExists(id))
            {
                return NotFound();
            }

            var entity = Db.CharacterAttributeTypes.Find(id);
            entity = Mapper.Map(dto, entity);

            entity.LastModified = DateTime.Now;
            Db.Entry(entity).State = EntityState.Modified;

            Db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="CharacterAttributeType"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(CharacterAttributeTypeDto))]
        [Route("characterattributetypes")]
        public IHttpActionResult PostCharacterAttributeType(CharacterAttributeTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Mapper.Map<CharacterAttributeTypeDto, CharacterAttributeType>(dto);
            entity.LastModified = DateTime.Now;
            Db.CharacterAttributeTypes.Add(entity);
            Db.SaveChanges();

            var newDto = Mapper.Map<CharacterAttributeType, CharacterAttributeTypeDto>(entity);
            return CreatedAtRoute("DefaultApi", new { controller = "CharacterAttributeTypes", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="CharacterAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
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

            return StatusCode(HttpStatusCode.OK);
        }

        private bool CharacterAttributeTypeExists(int id)
        {
            return Db.CharacterAttributeTypes.Count(e => e.Id == id) > 0;
        }
    }
}