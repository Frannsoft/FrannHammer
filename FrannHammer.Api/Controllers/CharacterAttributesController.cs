using System;
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
    /// Handles all individual <see cref="CharacterAttribute"/> operations.  
    /// </summary>
    [RoutePrefix("api")]
    public class CharacterAttributesController : BaseApiController
    {
        private const string CharacterAttributesRouteKey = "characterattributes";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="CharacterAttribute"/> controller to interact with the server. 
        /// </summary>
        public CharacterAttributesController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="CharacterAttribute"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeDto))]
        [ValidateModel]
        [Route(CharacterAttributesRouteKey)]
        public IHttpActionResult GetCharacterAttributes([FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<CharacterAttribute, CharacterAttributeDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="CharacterAttribute"/> by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeDto))]
        [ValidateModel]
        [Route(CharacterAttributesRouteKey + "/{id}")]
        public IHttpActionResult GetCharacterAttribute(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.Get<CharacterAttribute, CharacterAttributeDto>(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="CharacterAttribute"/> by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeDto))]
        [ValidateModel]
        [Route(CharacterAttributesRouteKey + "/name/{name}")]
        public IHttpActionResult GetCharacterAttributeByName(string name, [FromUri] string fields = "")
        {
            var content =
                _metadataService.GetAll<CharacterAttribute, CharacterAttributeDto>(
                    c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase), fields, false);
            return Ok(content);
        }

        /// <summary>
        /// Update a <see cref="CharacterAttribute"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ValidateModel]
        [Route(CharacterAttributesRouteKey + "/{id}")]
        public IHttpActionResult PutCharacterAttribute(int id, CharacterAttributeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<CharacterAttribute, CharacterAttributeDto>(id, dto);

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="CharacterAttribute"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ValidateModel]
        [Route(CharacterAttributesRouteKey)]
        public IHttpActionResult PostCharacterAttribute(CharacterAttributeDto dto)
        {
            var newDto = _metadataService.Add<CharacterAttribute, CharacterAttributeDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "CharacterAttributes", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="CharacterAttribute"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(CharacterAttributesRouteKey + "/{id}")]
        public IHttpActionResult DeleteCharacterAttribute(int id)
        {
            _metadataService.Delete<CharacterAttribute>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}