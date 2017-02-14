using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FrannHammer.Api.ActionFilterAttributes;
using FrannHammer.Api.Models;
using FrannHammer.Models;
using FrannHammer.Models.DTOs;
using FrannHammer.Services;

namespace FrannHammer.Api.Controllers
{
    /// <summary>
    /// Handles the backing types of <see cref="CharacterAttribute"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class CharacterAttributeTypesController : BaseApiController
    {
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="CharacterAttributeTypesController"/>.
        /// </summary>
        /// <param name="metadataService"></param>
        public CharacterAttributeTypesController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all of the <see cref="CharacterAttributeType"/> details.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeTypeDto))]
        [ValidateModel]
        [Route("characterattributetypes")]
        public IHttpActionResult GetCharacterAttributeTypes([FromUri] string fields = "")
        {
            var content =
                _metadataService.GetAll<CharacterAttributeType, CharacterAttributeTypeDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="CharacterAttributeType"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeTypeDto))]
        [ValidateModel]
        [Route("characterattributetypes/{id}")]
        public IHttpActionResult GetCharacterAttributeType(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.Get<CharacterAttributeType, CharacterAttributeTypeDto>(id, fields);

            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Update a <see cref="CharacterAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [ValidateModel]
        [Route("characterattributetypes/{id}")]
        public IHttpActionResult PutCharacterAttributeType(int id, CharacterAttributeTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<CharacterAttributeType, CharacterAttributeTypeDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="CharacterAttributeType"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(CharacterAttributeTypeDto))]
        [ValidateModel]
        [Route("characterattributetypes")]
        public IHttpActionResult PostCharacterAttributeType(CharacterAttributeTypeDto dto)
        {
            var newDto = _metadataService.Add<CharacterAttributeType, CharacterAttributeTypeDto>(dto);
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
            _metadataService.Delete<CharacterAttributeType>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}