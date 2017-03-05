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
    /// Handles operations dealing with <see cref="ThrowType"/>s.
    /// </summary>
    [RoutePrefix("api")]
    public class ThrowTypesController : BaseApiController
    {
        private const string ThrowTypesRouteKey = "ThrowTypes";
        private readonly IMetadataService _metadataService;

        /// <summary>
        /// Create a new <see cref="ThrowTypesController"/> to interact with the server.
        /// </summary>
        public ThrowTypesController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        /// <summary>
        /// Get all <see cref="Throw"/>s.
        /// </summary>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(ThrowTypeDto))]
        [ValidateModel]
        [Route(ThrowTypesRouteKey)]
        public IHttpActionResult GetThrowTypes([FromUri] string fields = "")
        {
            var content = _metadataService.GetAll<ThrowType, ThrowTypeDto>(fields);
            return Ok(content);
        }

        /// <summary>
        /// Get a specific <see cref="Throw"/>s details.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fields">Specify which specific pieces of the response model you need via comma-separated values. <para> 
        /// E.g., id,name to get back just the id and name.</para></param>
        /// <returns></returns>
        [ResponseType(typeof(ThrowTypeDto))]
        [ValidateModel]
        [Route(ThrowTypesRouteKey + "/{id}")]
        public IHttpActionResult GetThrowType(int id, [FromUri] string fields = "")
        {
            var content = _metadataService.Get<ThrowType, ThrowTypeDto>(id, fields);
            return ReturnResponse(content);
        }

        /// <summary>
        /// Update a <see cref="ThrowType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        [ValidateModel]
        [Route(ThrowTypesRouteKey + "/{id}")]
        public IHttpActionResult PutThrowType(int id, ThrowTypeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _metadataService.Update<ThrowType, ThrowTypeDto>(id, dto);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Create a new <see cref="ThrowType"/>.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [ResponseType(typeof(ThrowTypeDto))]
        [ValidateModel]
        [Route(ThrowTypesRouteKey)]
        public IHttpActionResult PostThrowType(ThrowTypeDto dto)
        {
            var newDto = _metadataService.Add<ThrowType, ThrowTypeDto>(dto);
            return CreatedAtRoute("DefaultApi", new { controller = "ThrowTypes", id = newDto.Id }, newDto);
        }

        /// <summary>
        /// Delete a <see cref="ThrowType"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RolesConstants.Admin)]
        [Route(ThrowTypesRouteKey + "/{id}")]
        public IHttpActionResult DeleteThrowType(int id)
        {
            _metadataService.Delete<ThrowType>(id);
            return StatusCode(HttpStatusCode.OK);
        }
    }
}