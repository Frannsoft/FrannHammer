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
    /// Handles the more broad types that can be assigned to other metadata in the Db.
    /// </summary>
    [RoutePrefix("api")]
    public class SmashAttributeTypesController : BaseApiController
    {
        private readonly ISmashAttributeTypeService _smashAttributeTypesService;

        /// <summary>
        /// Create a new <see cref="SmashAttributeTypesController"/> to interact with the server.
        /// </summary>
        public SmashAttributeTypesController(ISmashAttributeTypeService smashAttributeTypesService)
        {
            _smashAttributeTypesService = smashAttributeTypesService;
        }

        /// <summary>
        /// Get all of the stored <see cref="SmashAttributeType"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(SmashAttributeTypeDto))]
        [ValidateModel]
        [Route("smashattributetypes")]
        public IHttpActionResult GetSmashAttributeTypes([FromUri] string fields = "")
        {
            var content = _smashAttributeTypesService.GetAll<SmashAttributeType, SmashAttributeTypeDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="SmashAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(SmashAttributeTypeDto))]
        [ValidateModel]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult GetSmashAttributeType(int id, [FromUri] string fields = "")
        {
            var content = _smashAttributeTypesService.Get<SmashAttributeType, SmashAttributeTypeDto>(id, fields);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="SmashAttributeType"/> by name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(SmashAttributeTypeDto))]
        [ValidateModel]
        [Route("smashattributetypes/name/{name}")]
        public IHttpActionResult GetSmashAttributeTypeByName(string name, [FromUri] string fields = "")
        {
            var content = _smashAttributeTypesService.Get<SmashAttributeType, SmashAttributeTypeDto>(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase), fields, false);
            return content == null ? NotFound() : Ok(content);
        }

        /// <summary>
        /// Get back all of the <see cref="SmashAttributeType"/>s of a specific id
        /// as <see cref="CharacterAttribute"/> objects.  This call parses the returned 
        /// <see cref="CharacterAttribute"/>s into <see cref="CharacterAttributeRowDto"/>, 
        /// similar to how they are displayed on KuroganeHammer.com.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeRowDto))]
        [ValidateModel]
        [Route("smashattributetypes/{id}/characterattributes")]
        public IHttpActionResult GetAllCharacterAttributeOfSmashAttributeType(int id, [FromUri] string fields = "")
        {
            //issue #95 - https://github.com/Frannsoft/FrannHammer/issues/95
            var content = _smashAttributeTypesService.GetAllCharacterAttributeOfSmashAttributeType(id, fields);
            return Ok(content);
        }

        /// <summary>
        /// Get back all of the <see cref="SmashAttributeType"/>s of a specific name.
        /// as <see cref="CharacterAttribute"/> objects.  This call parses the returned 
        /// <see cref="CharacterAttribute"/>s into <see cref="CharacterAttributeRowDto"/>, 
        /// similar to how they are displayed on KuroganeHammer.com.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(CharacterAttributeRowDto))]
        [ValidateModel]
        [Route("smashattributetypes/name/{name}/characterattributes")]
        public IHttpActionResult GetAllCharacterAttributeOfSmashAttributeTypeByName(string name, [FromUri] string fields = "")
        {
            //issue #95 - https://github.com/Frannsoft/FrannHammer/issues/95
            var content = _smashAttributeTypesService.GetAllCharacterAttributeOfSmashAttributeType(name, fields);
            return Ok(content);
        }

        /// <summary>
        /// Update an existing <see cref="SmashAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(void))]
        [ValidateModel]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult PutSmashAttributeType(int id, SmashAttributeTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _smashAttributeTypesService.Update<SmashAttributeType, SmashAttributeTypeDto>(id, dto);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="SmashAttributeType"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(SmashAttributeType))]
        [ValidateModel]
        [Route("smashattributetypes")]
        public IHttpActionResult PostSmashAttributeType(SmashAttributeTypeDto dto)
        {
            var newDto = _smashAttributeTypesService.Add<SmashAttributeType, SmashAttributeTypeDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "SmashAttributeTypes", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete an existing <see cref="SmashAttributeType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route("smashattributetypes/{id}")]
        public IHttpActionResult DeleteSmashAttributeType(int id)
        {
            _smashAttributeTypesService.Delete<SmashAttributeType>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}